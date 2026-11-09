using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
namespace SmartInventroy.Web.Controllers.Api;
[ApiController, AutoValidateAntiforgeryToken, Route("api/v1/account")]
public class AccountApiController : ControllerBase
{
    private readonly OperationsService _service;
    public AccountApiController(OperationsService service) => _service = service;

    [AllowAnonymous, IgnoreAntiforgeryToken, HttpGet("csrf")]
    public IActionResult Csrf([Microsoft.AspNetCore.Mvc.FromServices] Microsoft.AspNetCore.Antiforgery.IAntiforgery antiforgery)
    {
        var tokens = antiforgery.GetAndStoreTokens(HttpContext);
        return Ok(new { requestToken = tokens.RequestToken });
    }

    [AllowAnonymous, IgnoreAntiforgeryToken, HttpGet("setup-status")]
    public IActionResult SetupStatus() => Ok(new { needsInitialAdmin = _service.NeedsFirstAdminSetup() });

    [AllowAnonymous, IgnoreAntiforgeryToken, HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ApiLoginRequest request)
    {
        var user = _service.ValidateLogin(request.UsernameOrEmail, request.Password);
        if (user == null) return Unauthorized(new { message = "Invalid credentials or inactive account." });
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()), new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email), new("Username", user.Username), new(ClaimTypes.Role, user.RoleName)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = request.RememberMe, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(request.RememberMe ? 72 : 8) });
        return Ok(new { message = "Signed in.", user = new { user.UserId, user.FullName, user.Username, user.Email, user.RoleName } });
    }

    [HttpGet("me")]
    public IActionResult Me() => Ok(new { userId = User.FindFirstValue(ClaimTypes.NameIdentifier), fullName = User.Identity?.Name,
        email = User.FindFirstValue(ClaimTypes.Email), username = User.FindFirstValue("Username"), role = User.FindFirstValue(ClaimTypes.Role) });

    [HttpPost("logout")]
    public async Task<IActionResult> Logout() { await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); return Ok(new { message = "Signed out." }); }

    [AllowAnonymous, IgnoreAntiforgeryToken, HttpPost("setup-admin")]
    public IActionResult SetupAdmin([FromBody] ApiSetupAdminRequest request)
    {
        if (!_service.NeedsFirstAdminSetup()) return Conflict(new { message = "An administrator has already been configured." });
        try { return _service.CreateFirstAdmin(request.FullName, request.Username, request.Email, request.Password)
            ? StatusCode(201, new { message = "Initial administrator created. Sign in to continue." })
            : Conflict(new { message = "Initial administrator could not be created." }); }
        catch (Exception e) { return Conflict(new { message = e.Message }); }
    }
}

public class ApiLoginRequest
{
    [Required] public string UsernameOrEmail { get; set; } = "";
    [Required] public string Password { get; set; } = "";
    public bool RememberMe { get; set; }
}
public class ApiSetupAdminRequest
{
    [Required, StringLength(150)] public string FullName { get; set; } = "";
    [Required, StringLength(50)] public string Username { get; set; } = "";
    [Required, EmailAddress, StringLength(100)] public string Email { get; set; } = "";
    [Required, MinLength(8)] public string Password { get; set; } = "";
}
