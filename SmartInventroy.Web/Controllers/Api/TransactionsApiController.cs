using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/transactions")]
public class TransactionsApiController : ControllerBase
{
    private readonly OperationsService _service;
    public TransactionsApiController(OperationsService service) => _service = service;
    [HttpGet] public IActionResult GetAll() => Ok(_service.Transactions());
}
