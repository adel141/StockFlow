using System.ComponentModel.DataAnnotations;
namespace SmartInventroy.Web.Models.Account;
public class SetupAdminViewModel
{
    [Required, StringLength(100)] public string FullName {
        get;
        set;
    }
    = "";
    [Required, StringLength(50)] public string Username {
        get;
        set;
    }
    = "";
    [Required, EmailAddress] public string Email {
        get;
        set;
    }
    = "";
    [Required, MinLength(8), DataType(DataType.Password)] public string Password {
        get;
        set;
    }
    = "";
    [Required, DataType(DataType.Password), Compare(nameof(Password))] public string ConfirmPassword {
        get;
        set;
    }
    = "";
}
