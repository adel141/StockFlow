using System;
using System.Collections.Generic;
namespace SmartInvetory.DAL.EF.Tables;
public partial class SalesOrderDetail
{
    public int SalesOrderDetailId { get; set; }
    public int SalesOrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? SubTotal { get; set; }
    public virtual Product Product { get; set; } = null!;
    public virtual SalesOrder SalesOrder { get; set; } = null!;
    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
