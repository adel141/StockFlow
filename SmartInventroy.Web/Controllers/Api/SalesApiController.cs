using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/sales")]
public class SalesApiController : ControllerBase
{
    private readonly OperationsService _service;
    public SalesApiController(OperationsService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.Sales());
    [HttpGet("{id:int}")] public IActionResult Get(int id) { var x = _service.Sale(id); return x == null ? NotFound() : Ok(x); }
    [HttpPost] public IActionResult Create(CreateOrderDto dto)
    {
        if (dto.PartyId is null or <= 0 || dto.WarehouseId <= 0 || dto.Items == null || dto.Items.Count == 0 || dto.Items.Any(x => x.ProductId <= 0 || x.Quantity <= 0 || x.UnitPrice < 0))
            return BadRequest(new { message = "A valid Customer PartyId, WarehouseId and at least one valid order item are required." });
        if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)) dto.CreatedBy = userId;
        try { return _service.CreateSale(dto) ? StatusCode(201, new { message = "Sale created and stock deducted." }) : Conflict(new { message = "Sale could not be completed. Check available stock." }); }
        catch (Exception e) { return Conflict(new { message = e.Message }); }
    }
}
