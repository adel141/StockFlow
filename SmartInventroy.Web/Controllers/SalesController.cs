using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartInventroy.BLL.Service;
using SmartInventroy.Web.Models.Operations;
using SmartInventory.Model.OperationsDto;
namespace SmartInventroy.Web.Controllers;
public class SalesController:Controller
{
    private readonly OperationsService _s;
    public SalesController(OperationsService s) {
        _s=s;
    }
    public IActionResult Index()=>View(_s.Sales());
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
            _s.CreateSale(ToDto(m));
            TempData["Success"]="Sale completed and stock deducted.";
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e) {
            ModelState.AddModelError("",e.Message);
            Load(m);
            return View(m);
        }
    }
    public IActionResult Details(int id) {
        var x=_s.Sale(id);
        return x==null?NotFound():View(x);
    }
    private void Load(OrderFormViewModel m) {
        m.Parties=_s.Customers().Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
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
