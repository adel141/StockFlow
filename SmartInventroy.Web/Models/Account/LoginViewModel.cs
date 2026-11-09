using System.ComponentModel.DataAnnotations;
namespace SmartInventroy.Web.Models.Account;
public class LoginViewModel
{
    [Required] public string UsernameOrEmail { get; set; } = "";
    [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}
