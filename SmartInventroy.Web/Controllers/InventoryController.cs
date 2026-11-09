using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartInventroy.BLL.Service;
using SmartInventroy.Web.Models.Operations;
using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Controllers;
public class InventoryController:Controller
{
    private readonly OperationsService _s;
    public InventoryController(OperationsService s) {
        _s=s;
    }
    public IActionResult Index() {
        return View(_s.Stock());
    }
    [HttpGet] public IActionResult Adjust() {
        var m=new StockAdjustmentViewModel();
        Load(m);
        return View(m);
    }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Adjust(StockAdjustmentViewModel m) {
        if(!ModelState.IsValid) {
            Load(m);
            return View(m);
        }
        try {
            _s.Adjust(new StockAdjustmentDto {
                ProductId=m.ProductId,WarehouseId=m.WarehouseId,Quantity=m.Quantity,Type=m.Type,Notes=m.Notes,CreatedBy=CurrentUserId()
            });
            TempData["Success"]="Stock adjusted successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e) {
            ModelState.AddModelError("",e.Message);
            Load(m);
            return View(m);
        }
    }
    private void Load(StockAdjustmentViewModel m) {
        m.Products=_s.Products().Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
        m.Warehouses=_s.Warehouses().Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
    }
    private int CurrentUserId() {
        var id=User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(id,out var n)?n:0;
    }
}
