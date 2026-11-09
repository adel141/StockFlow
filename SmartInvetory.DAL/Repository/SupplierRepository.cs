using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.EF.Tables;
using SmartInvetory.DAL.Interfaces;
namespace SmartInvetory.DAL.Repository;
public class SupplierRepository : Repository<Supplier, int>, ISupplierRepository
{
    public SupplierRepository(SmartInventoryDbContext context) : base(context) { }
    public override List<Supplier> GetAll() => _context.Suppliers.Include(x => x.PurchaseOrders).AsNoTracking().ToList();
}
