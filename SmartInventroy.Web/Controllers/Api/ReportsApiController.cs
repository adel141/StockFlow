using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/reports")]
public class ReportsApiController : ControllerBase
{
    private readonly OperationsService _service;
    public ReportsApiController(OperationsService service) => _service = service;
    [HttpGet("summary")] public IActionResult Summary() => Ok(_service.Dashboard());
    [HttpGet("stock")] public IActionResult Stock() => Ok(_service.Stock());
    [HttpGet("purchases")] public IActionResult Purchases() => Ok(_service.Purchases());
    [HttpGet("sales")] public IActionResult Sales() => Ok(_service.Sales());
    [HttpGet("transactions")] public IActionResult Transactions() => Ok(_service.Transactions());
}
