using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/purchases")]
public class PurchasesApiController : ControllerBase
{
    private readonly OperationsService _service;
    public PurchasesApiController(OperationsService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.Purchases());
    [HttpGet("{id:int}")] public IActionResult Get(int id) { var x = _service.Purchase(id); return x == null ? NotFound() : Ok(x); }
    [HttpPost] public IActionResult Create(CreateOrderDto dto)
    {
        var error = ValidateOrder(dto); if (error != null) return BadRequest(new { message = error });
        if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)) dto.CreatedBy = userId;
        try { return _service.CreatePurchase(dto) ? StatusCode(201, new { message = "Purchase order created and stock received." }) : Conflict(new { message = "Purchase could not be created." }); }
        catch (Exception e) { return Conflict(new { message = e.Message }); }
    }
    private static string? ValidateOrder(CreateOrderDto dto)
    {
        if (dto.PartyId is null or <= 0) return "A valid Supplier PartyId is required.";
        if (dto.WarehouseId <= 0) return "A valid WarehouseId is required.";
        if (dto.Items == null || dto.Items.Count == 0) return "At least one order item is required.";
        if (dto.Items.Any(x => x.ProductId <= 0 || x.Quantity <= 0 || x.UnitPrice < 0)) return "Each item needs a valid ProductId, positive Quantity and non-negative UnitPrice.";
        return null;
    }
}
