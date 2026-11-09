using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.EF.Tables;
using SmartInvetory.DAL.Interfaces;
namespace SmartInvetory.DAL.Repository;
public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
{
    public CustomerRepository(SmartInventoryDbContext context) : base(context) { }
    public override List<Customer> GetAll() => _context.Customers.Include(x => x.SalesOrders).AsNoTracking().ToList();
}
