using System;
using System.Collections.Generic;

namespace SmartInvetory.DAL.EF.Tables;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public string PurchaseOrderNumber { get; set; } = null!;

    public int SupplierId { get; set; }

    public int WarehouseId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateOnly? ExpectedDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
