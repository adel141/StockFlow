using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/notifications")]
public class NotificationsApiController : ControllerBase
{
    private readonly OperationsService _service;
    public NotificationsApiController(OperationsService service) => _service = service;
    [HttpGet("low-stock")] public IActionResult LowStock() => Ok(_service.LowStockNotifications());
}
