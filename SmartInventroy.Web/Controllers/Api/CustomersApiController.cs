using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.CrudDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/customers")]
public class CustomersApiController : ControllerBase
{
    private readonly CustomerService _service;
    public CustomersApiController(CustomerService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.GetAll());
    [HttpGet("{id:int}")] public IActionResult Get(int id) { var x = _service.Get(id); return x == null ? NotFound() : Ok(x); }
    [HttpPost] public IActionResult Create(CustomerDto dto) { dto.CustomerId = 0; return _service.Save(dto) ? StatusCode(201, new { message = "Customer created." }) : Conflict(); }
    [HttpPut("{id:int}")] public IActionResult Update(int id, CustomerDto dto) { if (id != dto.CustomerId) return BadRequest(); return _service.Save(dto) ? NoContent() : NotFound(); }
    [HttpDelete("{id:int}")] public IActionResult Delete(int id) { try { return _service.Delete(id) ? NoContent() : NotFound(); } catch (Exception e) { return Conflict(new { message = "Customer has related sales orders.", detail = e.Message }); } }
}
