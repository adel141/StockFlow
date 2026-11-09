using Microsoft.AspNetCore.Mvc;using SmartInventroy.BLL.Service;using SmartInventroy.Web.Models;using System.Diagnostics;
namespace SmartInventroy.Web.Controllers;
public class HomeController:Controller{private readonly OperationsService _s;public HomeController(OperationsService s){_s=s;}public IActionResult Index()=>View(_s.Dashboard());public IActionResult Privacy()=>View();[ResponseCache(Duration=0,Location=ResponseCacheLocation.None,NoStore=true)]public IActionResult Error()=>View(new ErrorViewModel{RequestId=Activity.Current?.Id??HttpContext.TraceIdentifier});}
