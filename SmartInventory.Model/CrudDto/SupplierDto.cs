using System.ComponentModel.DataAnnotations;
namespace SmartInventory.Model.CrudDto;
public class SupplierDto {
    public int SupplierId {
        get;
        set;
    }
    [Required, StringLength(150)] public string SupplierName {
        get;
        set;
    }
    = "";
    [StringLength(100)] public string? ContactPerson {
        get;
        set;
    }
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
    public bool IsActive {
        get;
        set;
    }
    = true;
    public int PurchaseOrderCount {
        get;
        set;
    }
}
