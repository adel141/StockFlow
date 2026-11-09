
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Model.ProductDto;
using SmartInventroy.BLL.Service;

namespace SmartInventroy.Web.Controllers
{ 
    public class ProductsController : Controller

    {

        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Products")]
        public IActionResult Index()
        
        {

            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet("Products/Edit/{id}")]

        public IActionResult Edit(int id)
        {
            var prodcuts = _productService.GetProductById(id);
            return View(prodcuts);

        }

        [HttpPost("Products/Edit/{id}")]
        public IActionResult Edit(int id, ProductListModel product)
        {
            product.ProductId = id;
            if (ModelState.IsValid)
            {
                if(_productService.UpdateProduct(id, product))
                return RedirectToAction("Index");
            }
            return View(product);
        }



        [HttpGet("/Products/Details/{id}")]
        public IActionResult Details(int id)
        {
            var product = _productService.GetProductByIdWithDetails(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
