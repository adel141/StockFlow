using System;
using System.Collections.Generic;
namespace SmartInvetory.DAL.EF.Tables;
public partial class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public string? Barcode { get; set; }
    public int CategoryId { get; set; }
    public string? Description { get; set; }
    public string Unit { get; set; } = null!;
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();
    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();
    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
