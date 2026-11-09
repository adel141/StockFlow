using Microsoft.AspNetCore.Mvc;
using SmartInventory.Model.CrudDto;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers;
public class CategoriesController : Controller
{
 private readonly CategoryService _service; public CategoriesController(CategoryService service)=>_service=service;
 public IActionResult Index()=>View(_service.GetAll());
 [HttpGet] public IActionResult Create()=>View(new CategoryDto{IsActive=true});
 [HttpPost,ValidateAntiForgeryToken] public IActionResult Create(CategoryDto model){if(!ModelState.IsValid)return View(model);try{_service.Save(model);TempData["Success"]="Category created successfully.";return RedirectToAction(nameof(Index));}catch(Exception ex){ModelState.AddModelError("",ex.Message);return View(model);}}
 [HttpGet] public IActionResult Edit(int id){var x=_service.Get(id);return x==null?NotFound():View(x);}
 [HttpPost,ValidateAntiForgeryToken] public IActionResult Edit(int id,CategoryDto model){if(id!=model.CategoryId)return BadRequest();if(!ModelState.IsValid)return View(model);try{if(!_service.Save(model))return NotFound();TempData["Success"]="Category updated successfully.";return RedirectToAction(nameof(Index));}catch(Exception ex){ModelState.AddModelError("",ex.Message);return View(model);}}
 [HttpPost,ValidateAntiForgeryToken] public IActionResult Delete(int id){try{_service.Delete(id);TempData["Success"]="Category deleted.";}catch{TempData["Error"]="Cannot delete a category that is used by products.";}return RedirectToAction(nameof(Index));}
}
