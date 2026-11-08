using Microsoft.AspNetCore.Mvc;
using SmartInventroy.Web.Models;
using System.Diagnostics;

namespace SmartInventroy.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<SmartInventory.Model.Product>
            {
                new SmartInventory.Model.Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 10.99m,
                    StockQuantity = 100,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 1,
                    UpdatedDate = DateTime.Now
                },
                new SmartInventory.Model.Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 19.99m,
                    StockQuantity = 50,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 1,
                    UpdatedDate = DateTime.Now
                },
                new SmartInventory.Model.Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Description = "Description for Product 3",
                    Price = 5.99m,
                    StockQuantity = 200,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 1,
                    UpdatedDate = DateTime.Now
                }
            };
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
