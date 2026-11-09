using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventory.Model.ProductDto;

namespace SmartInventroy.Web.Controllers.Api;

[ApiController, AutoValidateAntiforgeryToken]
[Route("api/v1/products")]
public class ProductsApiController : ControllerBase
{
    private readonly ProductService _products;
    public ProductsApiController(ProductService products) => _products = products;

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? search, [FromQuery] int? categoryId, [FromQuery] string? status)
    {
        var items = _products.GetAllProducts();
        if (!string.IsNullOrWhiteSpace(search))
            items = items.Where(x => x.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.Sku.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (x.Barcode ?? "").Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        if (categoryId.HasValue) items = items.Where(x => x.CategoryId == categoryId.Value).ToList();
        if (status == "active") items = items.Where(x => x.IsActive).ToList();
        if (status == "inactive") items = items.Where(x => !x.IsActive).ToList();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var item = _products.GetProductByIdWithDetails(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] ProductCreateDto model)
    {
        var validationError = ValidateProduct(model.ProductName, model.Sku, model.Unit, model.CategoryId, model.PurchasePrice, model.SellingPrice, model.ReorderLevel);
        if (validationError != null) return BadRequest(new { message = validationError });
        if (!_products.AddProduct(model)) return Conflict(new { message = "Product could not be created. Check SKU and barcode uniqueness." });
        return StatusCode(StatusCodes.Status201Created, new { message = "Product created." });
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] ProductUpdateDto model)
    {
        if (id != model.ProductId) return BadRequest(new { message = "Route id and ProductId must match." });
        var validationError = ValidateProduct(model.ProductName, model.SKU, model.Unit, model.CategoryId, model.PurchasePrice, model.SellingPrice, model.ReorderLevel);
        if (validationError != null) return BadRequest(new { message = validationError });
        return _products.UpdateProduct(id, model) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try { return _products.DeleteProduct(id) ? NoContent() : NotFound(); }
        catch (Exception ex) { return Conflict(new { message = "Product is referenced by inventory records.", detail = ex.Message }); }
    }

    private static string? ValidateProduct(string? name, string? sku, string? unit, int categoryId, decimal purchasePrice, decimal sellingPrice, int reorderLevel)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sku) || string.IsNullOrWhiteSpace(unit)) return "Product name, SKU and unit are required.";
        if (categoryId <= 0) return "A valid CategoryId is required.";
        if (purchasePrice < 0 || sellingPrice < 0 || reorderLevel < 0) return "Prices and reorder level cannot be negative.";
        return null;
    }

    [HttpGet("active")]
    public IActionResult Active() => Ok(_products.GetActiveProducts());
    [HttpGet("inactive")]
    public IActionResult Inactive() => Ok(_products.GetInactiveProducts());
    [HttpGet("low-stock")]
    public IActionResult LowStock() => Ok(_products.GetLowStockProducts());
    [HttpGet("by-category/{categoryId:int}")]
    public IActionResult ByCategory(int categoryId) => Ok(_products.GetProductsByCategory(categoryId));
    [HttpGet("search/name")]
    public IActionResult ByName([FromQuery] string name) => string.IsNullOrWhiteSpace(name) ? BadRequest(new { message = "name is required." }) : Ok(_products.GetProductsByName(name));
    [HttpGet("search/sku")]
    public IActionResult BySku([FromQuery] string sku) => string.IsNullOrWhiteSpace(sku) ? BadRequest(new { message = "sku is required." }) : Ok(_products.GetProductsBySku(sku));
    [HttpGet("search/barcode")]
    public IActionResult ByBarcode([FromQuery] string barcode) => string.IsNullOrWhiteSpace(barcode) ? BadRequest(new { message = "barcode is required." }) : Ok(_products.GetProductsByBarcode(barcode));
}
