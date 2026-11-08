
using Microsoft.AspNetCore.Mvc;

namespace SmartInventory.Web.Controllers
{
    public class CustomersController : Controller
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
        public IActionResult Edit(int id)
        {
            return View();
        }
    }
}
