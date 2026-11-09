using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartInventroy.BLL.Service;
using SmartInventroy.Web.Models.Operations;
using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Controllers;
public class PurchasesController:Controller
{
    private readonly OperationsService _s;
    public PurchasesController(OperationsService s) {
        _s=s;
    }
    public IActionResult Index()=>View(_s.Purchases());
    [HttpGet] public IActionResult Create() {
        var m=new OrderFormViewModel();
        Load(m);
        return View(m);
    }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Create(OrderFormViewModel m) {
        if(!ModelState.IsValid) {
            Load(m);
            return View(m);
        }
        try {
            _s.CreatePurchase(ToDto(m));
            TempData["Success"]="Purchase received and stock updated.";
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e) {
            ModelState.AddModelError("",e.Message);
            Load(m);
            return View(m);
        }
    }
    public IActionResult Details(int id) {
        var x=_s.Purchase(id);
        return x==null?NotFound():View(x);
    }
    private void Load(OrderFormViewModel m) {
        m.Parties=_s.Suppliers().Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
        m.Warehouses=_s.Warehouses().Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
        m.Products=_s.Products().Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
    }
    private CreateOrderDto ToDto(OrderFormViewModel m)=>new() {
PartyId=m
            .PartyId,
            WarehouseId=m
            .WarehouseId,
            OrderDate=m
            .OrderDate,
            ExpectedDate=m
            .ExpectedDate
            .HasValue?DateOnly
            .FromDateTime(m
            .ExpectedDate
            .Value):null,
            Notes=m
            .Notes,
            CreatedBy=CurrentUserId(),
            Items=m
            .Items
            .Select(x=>new CreateOrderLineDto {
            ProductId=x.ProductId,Quantity=x.Quantity,UnitPrice=x.UnitPrice
        }).ToList()
    };
    private int CurrentUserId() {
        var id=User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(id,out var n)?n:0;
    }
}
