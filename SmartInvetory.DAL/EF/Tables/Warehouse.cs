using System;
using System.Collections.Generic;

namespace SmartInvetory.DAL.EF.Tables;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string WarehouseName { get; set; } = null!;

    public string? Location { get; set; }

    public string? ManagerName { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
