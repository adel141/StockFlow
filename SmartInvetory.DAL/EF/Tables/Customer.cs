using System;
using System.Collections.Generic;
namespace SmartInvetory.DAL.EF.Tables;
public partial class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}
