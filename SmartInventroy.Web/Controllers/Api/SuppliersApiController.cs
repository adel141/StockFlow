using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.CrudDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/suppliers")]
public class SuppliersApiController : ControllerBase
{
    private readonly SupplierService _service;
    public SuppliersApiController(SupplierService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.GetAll());
    [HttpGet("{id:int}")] public IActionResult Get(int id) { var x = _service.Get(id); return x == null ? NotFound() : Ok(x); }
    [HttpPost] public IActionResult Create(SupplierDto dto) { dto.SupplierId = 0; return _service.Save(dto) ? StatusCode(201, new { message = "Supplier created." }) : Conflict(); }
    [HttpPut("{id:int}")] public IActionResult Update(int id, SupplierDto dto) { if (id != dto.SupplierId) return BadRequest(); return _service.Save(dto) ? NoContent() : NotFound(); }
    [HttpDelete("{id:int}")] public IActionResult Delete(int id) { try { return _service.Delete(id) ? NoContent() : NotFound(); } catch (Exception e) { return Conflict(new { message = "Supplier has related purchase orders.", detail = e.Message }); } }
}
