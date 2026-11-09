using Microsoft.AspNetCore.Mvc;using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers;public class SettingsController:Controller{private readonly OperationsService _s;public SettingsController(OperationsService s){_s=s;}public IActionResult Index(){ViewBag.WarehouseCount=_s.Warehouses().Count;ViewBag.RoleCount=_s.Roles().Count;return View();}}
