using Microsoft.AspNetCore.Mvc; using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers;
public class TransactionsController:Controller{private readonly OperationsService _s;public TransactionsController(OperationsService s){_s=s;}public IActionResult Index()=>View(_s.Transactions());}
