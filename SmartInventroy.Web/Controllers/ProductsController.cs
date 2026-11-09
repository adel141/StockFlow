using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartInventory.Model.ProductDto;
using SmartInventory.Model.CrudDto;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers;
public class ProductsController:Controller
{
    private readonly ProductService _products;
    private readonly CategoryService _categories;
    public ProductsController(ProductService products,CategoryService categories) {
        _products=products;
        _categories=categories;
    }
    private void LoadCategories() {
        ViewBag.Categories=_categories.GetAll().Where(c=>c.IsActive).Select(c=>new SelectListItem(c.CategoryName,c.CategoryId.ToString())).ToList();
    }
    public IActionResult Index(string? search,int? categoryId,string? status) {
        var list=_productServiceList();
if(!string
            .IsNullOrWhiteSpace(search))list=list
            .Where(p=>p
            .ProductName
            .Contains(search,
            StringComparison
            .OrdinalIgnoreCase)||p
            .Sku
            .Contains(search,
            StringComparison
            .OrdinalIgnoreCase)||(p
            .Barcode??"")
            .Contains(search,
            StringComparison
            .OrdinalIgnoreCase))
            .ToList();
        if(categoryId.HasValue)list=list.Where(p=>p.CategoryId==categoryId).ToList();
        if(status=="active")list=list.Where(p=>p.IsActive).ToList();
        if(status=="inactive")list=list.Where(p=>!p.IsActive).ToList();
        ViewBag.Search=search;
        ViewBag.CategoryId=categoryId;
        ViewBag.Status=status;
        ViewBag.Categories=_categories.GetAll();
        return View(list);
    }
    private List<ProductListModel> _productServiceList()=>_products.GetAllProducts();
    [HttpGet] public IActionResult Create() {
        LoadCategories();
        return View(new ProductCreateDto());
    }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Create(ProductCreateDto model) {
        if(!ModelState.IsValid) {
            LoadCategories();
            return View(model);
        }
        try {
            if(_products.AddProduct(model)) {
                TempData["Success"]="Product created successfully.";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("","Product could not be saved.");
        }
        catch(Exception ex) {
            ModelState.AddModelError("",ex.Message);
        }
        LoadCategories();
        return View(model);
    }
    [HttpGet] public IActionResult Edit(int id) {
        var d=_products.GetProductByIdWithDetails(id);
        if(d==null)return NotFound();
        LoadCategories();
        var m=new ProductUpdateDto {
ProductId=d
                .ProductId,
                ProductName=d
                .ProductName,
                SKU=d
                .SKU,
                Barcode=d
                .Barcode,
                CategoryId=d
                .CategoryId,
                Description=d
                .Description,
                Unit=d
                .Unit,
                PurchasePrice=d
                .PurchasePrice,
                SellingPrice=d
                .SellingPrice,
                ReorderLevel=d
                .ReorderLevel,
                IsActive=d
                .IsActive,
                CreatedAt=d
                .CreatedAt
        };
        return View(m);
    }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Edit(int id,ProductUpdateDto model) {
        if(id!=model.ProductId)return BadRequest();
        if(!ModelState.IsValid) {
            LoadCategories();
            return View(model);
        }
        try {
            if(_products.UpdateProduct(id,model)) {
                TempData["Success"]="Product updated.";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        catch(Exception ex) {
            ModelState.AddModelError("",ex.Message);
            LoadCategories();
            return View(model);
        }
    }
    public IActionResult Details(int id) {
        var d=_products.GetProductByIdWithDetails(id);
        return d==null?NotFound():View(d);
    }
    [HttpGet] public IActionResult Delete(int id) {
        var p=_products.GetProductByIdWithDetails(id);
        return p==null?NotFound():View(p);
    }
    [HttpPost,ActionName("Delete"),ValidateAntiForgeryToken] public IActionResult DeleteConfirmed(int id) {
        try {
            if(!_products.DeleteProduct(id))return NotFound();
            TempData["Success"]="Product deleted.";
        }
        catch {
            TempData["Error"]="This product is referenced by stock or order records and cannot be deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
