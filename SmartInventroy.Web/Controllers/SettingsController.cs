
using Microsoft.AspNetCore.Mvc;

namespace SmartInventory.Web.Controllers
{
    public class SettingsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
