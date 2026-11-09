using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/settings"), Authorize(Policy = "AdminOnly")]
public class SettingsApiController : ControllerBase
{
    private readonly OperationsService _service;
    public SettingsApiController(OperationsService service) => _service = service;
    [HttpGet("overview")] public IActionResult Overview() => Ok(new { warehouseCount = _service.Warehouses().Count, roleCount = _service.Roles().Count });
}
