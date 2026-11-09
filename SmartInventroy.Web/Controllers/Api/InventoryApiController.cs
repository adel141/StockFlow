using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/inventory")]
public class InventoryApiController : ControllerBase
{
    private readonly OperationsService _service;
    public InventoryApiController(OperationsService service) => _service = service;
    [HttpGet("stock")] public IActionResult Stock() => Ok(_service.Stock());
    [HttpGet("transactions")] public IActionResult Transactions() => Ok(_service.Transactions());
    [HttpPost("adjustments")]
    public IActionResult Adjust(StockAdjustmentDto dto)
    {
        if (dto.ProductId <= 0 || dto.WarehouseId <= 0 || dto.Quantity <= 0 ||
            (dto.Type != "AdjustmentIn" && dto.Type != "AdjustmentOut"))
            return BadRequest(new { message = "Valid ProductId, WarehouseId, positive Quantity and Type (AdjustmentIn or AdjustmentOut) are required." });
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userId, out var id)) dto.CreatedBy = id;
        try { return _service.Adjust(dto) ? StatusCode(201, new { message = "Stock adjustment recorded." }) : Conflict(new { message = "Stock adjustment failed." }); }
        catch (Exception e) { return Conflict(new { message = e.Message }); }
    }
}
