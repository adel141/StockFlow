using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.CrudDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/warehouses")]
public class WarehousesApiController : ControllerBase
{
    private readonly WarehouseService _service;
    public WarehousesApiController(WarehouseService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.GetAll());
    [HttpGet("{id:int}")] public IActionResult Get(int id) { var x = _service.Get(id); return x == null ? NotFound() : Ok(x); }
    [HttpPost] public IActionResult Create(WarehouseDto dto) { dto.WarehouseId = 0; return _service.Save(dto) ? StatusCode(201, new { message = "Warehouse created." }) : Conflict(); }
    [HttpPut("{id:int}")] public IActionResult Update(int id, WarehouseDto dto) { if (id != dto.WarehouseId) return BadRequest(); return _service.Save(dto) ? NoContent() : NotFound(); }
    [HttpDelete("{id:int}")] public IActionResult Delete(int id) { try { return _service.Delete(id) ? NoContent() : NotFound(); } catch (Exception e) { return Conflict(new { message = "Warehouse has related stock or orders.", detail = e.Message }); } }
}
