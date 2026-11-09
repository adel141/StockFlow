
using Microsoft.AspNetCore.Mvc;

namespace SmartInventory.Web.Controllers
{
    [Route("Warehouse")]
    public class WarehousesController : Controller
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

        [HttpGet("/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View();
        }
    }
}
