using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/dashboard")]
public class DashboardApiController : ControllerBase
{
    private readonly OperationsService _service;
    public DashboardApiController(OperationsService service) => _service = service;
    [HttpGet] public IActionResult Get() => Ok(_service.Dashboard());
}
