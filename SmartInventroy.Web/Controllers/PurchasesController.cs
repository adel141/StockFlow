
using Microsoft.AspNetCore.Mvc;

namespace SmartInventory.Web.Controllers
{
    public class PurchasesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
