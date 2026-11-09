using System.ComponentModel.DataAnnotations;
namespace SmartInventory.Model.OperationsDto;
public class SelectOptionDto {
    public int Id {
        get;
        set;
    }
    public string Name {
        get;
        set;
    }
    = "";
}
public class StockRowDto {
    public int ProductId {
        get;
        set;
    }
    public string ProductName {
        get;
        set;
    }
    ="";
    public string Sku {
        get;
        set;
    }
    ="";
    public int WarehouseId {
        get;
        set;
    }
    public string WarehouseName {
        get;
        set;
    }
    ="";
    public int Quantity {
        get;
        set;
    }
    public int ReorderLevel {
        get;
        set;
    }
    public bool IsLowStock => Quantity <= ReorderLevel;
}
public class TransactionRowDto {
    public long Id {
        get;
        set;
    }
    public DateTime Date {
        get;
        set;
    }
    public string ProductName {
        get;
        set;
    }
    ="";
    public string WarehouseName {
        get;
        set;
    }
    ="";
    public string Type {
        get;
        set;
    }
    ="";
    public int Quantity {
        get;
        set;
    }
    public string? Reference {
        get;
        set;
    }
    public string? Notes {
        get;
        set;
    }
}
public class OrderRowDto {
    public int Id {
        get;
        set;
    }
    public string Number {
        get;
        set;
    }
    ="";
    public DateTime Date {
        get;
        set;
    }
    public string PartyName {
        get;
        set;
    }
    ="";
    public string WarehouseName {
        get;
        set;
    }
    ="";
    public string Status {
        get;
        set;
    }
    ="";
    public decimal Total {
        get;
        set;
    }
}
public class OrderDetailRowDto {
    public int ProductId {
        get;
        set;
    }
    public string ProductName {
        get;
        set;
    }
    ="";
    public int Quantity {
        get;
        set;
    }
    public decimal UnitPrice {
        get;
        set;
    }
    public decimal SubTotal => Quantity * UnitPrice;
}
public class OrderDetailsDto {
    public int Id {
        get;
        set;
    }
    public string Number {
        get;
        set;
    }
    ="";
    public DateTime Date {
        get;
        set;
    }
    public string PartyName {
        get;
        set;
    }
    ="";
    public string WarehouseName {
        get;
        set;
    }
    ="";
    public string Status {
        get;
        set;
    }
    ="";
    public decimal Total {
        get;
        set;
    }
    public string? Notes {
        get;
        set;
    }
    public List<OrderDetailRowDto> Items {
        get;
        set;
    }
    =new();
}
public class CreateOrderDto {
    public int? PartyId {
        get;
        set;
    }
    public int WarehouseId {
        get;
        set;
    }
    public DateTime OrderDate {
        get;
        set;
    }
    =DateTime.Today;
    public DateOnly? ExpectedDate {
        get;
        set;
    }
    public string? Notes {
        get;
        set;
    }
    public int CreatedBy {
        get;
        set;
    }
    =1;
    public List<CreateOrderLineDto> Items {
        get;
        set;
    }
    =new();
}
public class CreateOrderLineDto {
    public int ProductId {
        get;
        set;
    }
    public int Quantity {
        get;
        set;
    }
    public decimal UnitPrice {
        get;
        set;
    }
}
public class StockAdjustmentDto {
    public int ProductId {
        get;
        set;
    }
    public int WarehouseId {
        get;
        set;
    }
    public int Quantity {
        get;
        set;
    }
    public string Type {
        get;
        set;
    }
    ="AdjustmentIn";
    public string? Notes {
        get;
        set;
    }
    public int CreatedBy {
        get;
        set;
    }
    =1;
}
public class DashboardDto {
    public int ProductCount {
        get;
        set;
    }
    public int CategoryCount {
        get;
        set;
    }
    public int SupplierCount {
        get;
        set;
    }
    public int CustomerCount {
        get;
        set;
    }
    public int LowStockCount {
        get;
        set;
    }
    public int WarehouseCount {
        get;
        set;
    }
    public decimal PurchaseTotal {
        get;
        set;
    }
    public decimal SalesTotal {
        get;
        set;
    }
    public List<StockRowDto> LowStock {
        get;
        set;
    }
    =new();
    public List<TransactionRowDto> RecentTransactions {
        get;
        set;
    }
    =new();
}
public class UserDto {
    public int UserId {
        get;
        set;
    }
    [Required] public string FullName {
        get;
        set;
    }
    ="";
    [Required] public string Username {
        get;
        set;
    }
    ="";
    [Required,EmailAddress] public string Email {
        get;
        set;
    }
    ="";
    public int RoleId {
        get;
        set;
    }
    public string RoleName {
        get;
        set;
    }
    ="";
    public bool IsActive {
        get;
        set;
    }
    =true;
    [MinLength(8)] public string? Password {
        get;
        set;
    }
}
public class LoginUserDto {
    public int UserId {
        get;
        set;
    }
    public string FullName {
        get;
        set;
    }
    ="";
    public string Username {
        get;
        set;
    }
    ="";
    public string Email {
        get;
        set;
    }
    ="";
    public string PasswordHash {
        get;
        set;
    }
    ="";
    public bool IsActive {
        get;
        set;
    }
    public string RoleName {
        get;
        set;
    }
    ="";
}
