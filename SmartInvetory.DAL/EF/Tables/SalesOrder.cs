using System;
using System.Collections.Generic;

namespace SmartInvetory.DAL.EF.Tables;

public partial class SalesOrder
{
    public int SalesOrderId { get; set; }

    public string SalesOrderNumber { get; set; } = null!;

    public int? CustomerId { get; set; }

    public int WarehouseId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
