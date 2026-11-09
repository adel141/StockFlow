using Microsoft.AspNetCore.Mvc;
using SmartInventory.Model.CrudDto;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers;
public class CustomersController:Controller {
    private readonly CustomerService _s;
    public CustomersController(CustomerService s)=>_s=s;
    public IActionResult Index()=>View(_s.GetAll());
    public IActionResult Details(int id) {
        var item=_s.Get(id);
        return item==null?NotFound():View(item);
    }
    [HttpGet] public IActionResult Create()=>View(new CustomerDto());
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Create(CustomerDto m) {
        if(!ModelState.IsValid)return View(m);
        try {
            _s.Save(m);
            TempData["Success"]="Customer created.";
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e) {
            ModelState.AddModelError("",e.Message);
            return View(m);
        }
    }
    [HttpGet] public IActionResult Edit(int id) {
        var x=_s.Get(id);
        return x==null?NotFound():View(x);
    }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Edit(int id,CustomerDto m) {
        if(id!=m.CustomerId)return BadRequest();
        if(!ModelState.IsValid)return View(m);
        try {
            if(!_s.Save(m))return NotFound();
            TempData["Success"]="Customer updated.";
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e) {
            ModelState.AddModelError("",e.Message);
            return View(m);
        }
    }
    [HttpPost,ValidateAntiForgeryToken] public IActionResult Delete(int id) {
        try {
            _s.Delete(id);
            TempData["Success"]="Customer deleted.";
        }
        catch {
            TempData["Error"]="Cannot delete a customer with sales orders.";
        }
        return RedirectToAction(nameof(Index));
    }
}
