using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.CrudDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/categories")]
public class CategoriesApiController : ControllerBase
{
    private readonly CategoryService _service;
    public CategoriesApiController(CategoryService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.GetAll());
    [HttpGet("{id:int}")] public IActionResult Get(int id) { var x = _service.Get(id); return x == null ? NotFound() : Ok(x); }
    [HttpPost] public IActionResult Create(CategoryDto dto) { dto.CategoryId = 0; return _service.Save(dto) ? StatusCode(201, new { message = "Category created." }) : Conflict(new { message = "Category could not be created." }); }
    [HttpPut("{id:int}")] public IActionResult Update(int id, CategoryDto dto) { if (id != dto.CategoryId) return BadRequest(); return _service.Save(dto) ? NoContent() : NotFound(); }
    [HttpDelete("{id:int}")] public IActionResult Delete(int id) { try { return _service.Delete(id) ? NoContent() : NotFound(); } catch (Exception e) { return Conflict(new { message = "Category is in use.", detail = e.Message }); } }
}
