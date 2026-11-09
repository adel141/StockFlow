using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventroy.BLL.Service;
using SmartInventroy.Web.Models.Account;
namespace SmartInventroy.Web.Controllers;
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly OperationsService _service;
    public AccountController(OperationsService service) => _service = service;
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
        ViewBag.NeedsSetup = _service.NeedsFirstAdminSetup();
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = _service.ValidateLogin(model.UsernameOrEmail, model.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username/email or password, or the account is inactive.");
            ViewBag.NeedsSetup = _service.NeedsFirstAdminSetup();
            return View(model);
        }
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email),
            new("Username", user.Username),
            new(ClaimTypes.Role, user.RoleName)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(model.RememberMe ? 72 : 8)
            });
        if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            return Redirect(model.ReturnUrl);
        return RedirectToAction("Index", "Home");
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }
    [HttpGet]
    public IActionResult SetupAdmin()
    {
        if (!_service.NeedsFirstAdminSetup()) return RedirectToAction(nameof(Login));
        return View(new SetupAdminViewModel());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult SetupAdmin(SetupAdminViewModel model)
    {
        if (!_service.NeedsFirstAdminSetup()) return RedirectToAction(nameof(Login));
        if (!ModelState.IsValid) return View(model);
        try
        {
            if (!_service.CreateFirstAdmin(model.FullName, model.Username, model.Email, model.Password))
            {
                ModelState.AddModelError("", "The initial administrator could not be created. Refresh and try again.");
                return View(model);
            }
            TempData["Success"] = "Administrator account created. You can now sign in.";
            return RedirectToAction(nameof(Login));
        }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); return View(model); }
    }
    [HttpGet]
    public IActionResult AccessDenied() => View();
}
