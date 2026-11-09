using System;
using System.Collections.Generic;

namespace SmartInvetory.DAL.EF.Tables;

public partial class PurchaseOrderDetail
{
    public int PurchaseOrderDetailId { get; set; }

    public int PurchaseOrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
