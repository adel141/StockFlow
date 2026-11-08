
using Microsoft.AspNetCore.Mvc;

namespace SmartInventory.Web.Controllers
{
    public class ReportsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
