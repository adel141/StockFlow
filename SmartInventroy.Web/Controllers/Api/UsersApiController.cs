using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/users"), Authorize(Policy = "AdminOnly")]
public class UsersApiController : ControllerBase
{
    private readonly OperationsService _service;
    public UsersApiController(OperationsService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.Users());
    [HttpGet("roles")] public IActionResult Roles() => Ok(_service.Roles());
    [HttpGet("{id:int}")] public IActionResult Get(int id) { var x = _service.User(id); return x == null ? NotFound() : Ok(x); }
    [HttpPost] public IActionResult Create(UserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 8) return BadRequest(new { message = "A password of at least 8 characters is required." });
        dto.UserId = 0;
        try { return _service.SaveUser(dto) ? StatusCode(201, new { message = "User created." }) : Conflict(new { message = "User could not be created." }); }
        catch (Exception e) { return Conflict(new { message = e.Message }); }
    }
    [HttpPut("{id:int}")] public IActionResult Update(int id, UserDto dto)
    {
        if (id != dto.UserId) return BadRequest(new { message = "Route id and UserId must match." });
        try { return _service.SaveUser(dto) ? NoContent() : NotFound(); }
        catch (Exception e) { return Conflict(new { message = e.Message }); }
    }
    [HttpDelete("{id:int}")] public IActionResult Delete(int id)
    {
        if (int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out var currentId) && currentId == id)
            return BadRequest(new { message = "You cannot delete your own signed-in account." });
        try { return _service.DeleteUser(id) ? NoContent() : NotFound(); }
        catch (Exception e) { return Conflict(new { message = e.Message }); }
    }
}
