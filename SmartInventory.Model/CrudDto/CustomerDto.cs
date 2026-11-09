using System.ComponentModel.DataAnnotations;
namespace SmartInventory.Model.CrudDto;
public class CustomerDto {
    public int CustomerId {
        get;
        set;
    }
    [Required, StringLength(150)] public string CustomerName {
        get;
        set;
    }
    = "";
    [StringLength(30)] public string? Phone {
        get;
        set;
    }
    [EmailAddress, StringLength(100)] public string? Email {
        get;
        set;
    }
    [StringLength(300)] public string? Address {
        get;
        set;
    }
    public int SalesOrderCount {
        get;
        set;
    }
}
