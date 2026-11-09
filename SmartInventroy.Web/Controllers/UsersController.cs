using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Mvc.Rendering;using SmartInventroy.BLL.Service;using SmartInventroy.Web.Models.Operations;
namespace SmartInventroy.Web.Controllers;
public class UsersController:Controller
{
 private readonly OperationsService _s;public UsersController(OperationsService s){_s=s;}public IActionResult Index()=>View(_s.Users());
 [HttpGet]public IActionResult Create(){var m=new UserFormViewModel();Load(m);return View(m);}
 [HttpPost,ValidateAntiForgeryToken]public IActionResult Create(UserFormViewModel m){if(!ModelState.IsValid){Load(m);return View(m);}try{_s.SaveUser(m.User);TempData["Success"]="User created.";return RedirectToAction(nameof(Index));}catch(Exception e){ModelState.AddModelError("",e.Message);Load(m);return View(m);}}
 [HttpGet]public IActionResult Edit(int id){var u=_s.User(id);if(u==null)return NotFound();var m=new UserFormViewModel{User=u};Load(m);return View(m);}
 [HttpPost,ValidateAntiForgeryToken]public IActionResult Edit(UserFormViewModel m){if(!ModelState.IsValid){Load(m);return View(m);}try{_s.SaveUser(m.User);TempData["Success"]="User updated.";return RedirectToAction(nameof(Index));}catch(Exception e){ModelState.AddModelError("",e.Message);Load(m);return View(m);}}
 [HttpPost,ValidateAntiForgeryToken]public IActionResult Delete(int id){try{_s.DeleteUser(id);TempData["Success"]="User deleted.";}catch(Exception e){TempData["Error"]=e.Message;}return RedirectToAction(nameof(Index));}
 private void Load(UserFormViewModel m)=>m.Roles=_s.Roles().Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
}
