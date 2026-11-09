using System.ComponentModel.DataAnnotations;
namespace SmartInventory.Model.CrudDto;
public class WarehouseDto {
    public int WarehouseId {
        get;
        set;
    }
    [Required, StringLength(150)] public string WarehouseName {
        get;
        set;
    }
    = "";
    [StringLength(250)] public string? Location {
        get;
        set;
    }
    [StringLength(150)] public string? ManagerName {
        get;
        set;
    }
    [StringLength(30)] public string? Phone {
        get;
        set;
    }
    public bool IsActive {
        get;
        set;
    }
    = true;
    public int ProductCount {
        get;
        set;
    }
}
