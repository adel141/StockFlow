using Microsoft.AspNetCore.Mvc;using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers;public class ReportsController:Controller{private readonly OperationsService _s;public ReportsController(OperationsService s){_s=s;}public IActionResult Index(){ViewBag.Stock=_s.Stock();ViewBag.Purchases=_s.Purchases();ViewBag.Sales=_s.Sales();return View(_s.Dashboard());}}
