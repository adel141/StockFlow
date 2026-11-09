using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.EF.Tables;
using SmartInvetory.DAL.Interfaces;
namespace SmartInvetory.DAL.Repository;
public class WarehouseRepository : Repository<Warehouse, int>, IWarehouseRepository
{
    public WarehouseRepository(SmartInventoryDbContext context) : base(context) { }
    public override List<Warehouse> GetAll() => _context.Warehouses.Include(x => x.StockBalances).AsNoTracking().ToList();
}
