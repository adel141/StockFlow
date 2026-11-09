using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SmartInventroy.Web.Models.Operations;
public class OrderFormViewModel
{
 [Display(Name="Supplier / Customer")] public int? PartyId {get;set;}
 [Required] public int WarehouseId {get;set;}
 [DataType(DataType.Date)] public DateTime OrderDate {get;set;}=DateTime.Today;
 [DataType(DataType.Date)] public DateTime? ExpectedDate {get;set;}
 public string? Notes {get;set;}
 public List<OrderLineViewModel> Items {get;set;}=new(){new()};
 public List<SelectListItem> Parties {get;set;}=new(); public List<SelectListItem> Warehouses {get;set;}=new(); public List<SelectListItem> Products {get;set;}=new();
}
public class OrderLineViewModel { [Range(1,int.MaxValue)] public int ProductId {get;set;} [Range(1,int.MaxValue)] public int Quantity {get;set;}=1; [Range(0,double.MaxValue)] public decimal UnitPrice {get;set;} }
public class StockAdjustmentViewModel { [Range(1,int.MaxValue)] public int ProductId {get;set;} [Range(1,int.MaxValue)] public int WarehouseId {get;set;} [Range(1,int.MaxValue)] public int Quantity {get;set;} public string Type {get;set;}="AdjustmentIn"; public string? Notes {get;set;} public List<SelectListItem> Products {get;set;}=new(); public List<SelectListItem> Warehouses {get;set;}=new(); }
