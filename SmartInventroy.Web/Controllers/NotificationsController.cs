using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers;
public class NotificationsController : Controller
{
    private readonly OperationsService _service;
    public NotificationsController(OperationsService service) => _service = service;
    [HttpGet]
    public IActionResult Panel() => PartialView("_NotificationPanel", _service.LowStockNotifications());
}
