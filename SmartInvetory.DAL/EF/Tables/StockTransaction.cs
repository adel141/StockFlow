using System;
using System.Collections.Generic;

namespace SmartInvetory.DAL.EF.Tables;

public partial class StockTransaction
{
    public long StockTransactionId { get; set; }

    public int ProductId { get; set; }

    public int WarehouseId { get; set; }

    public string TransactionType { get; set; } = null!;

    public int Quantity { get; set; }

    public string? ReferenceNumber { get; set; }

    public int? PurchaseOrderDetailId { get; set; }

    public int? SalesOrderDetailId { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Notes { get; set; }

    public int CreatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual PurchaseOrderDetail? PurchaseOrderDetail { get; set; }

    public virtual SalesOrderDetail? SalesOrderDetail { get; set; }

    public virtual Warehouse Warehouse { get; set; } = null!;
}
