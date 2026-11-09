using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.EF.Tables;
using SmartInvetory.DAL.Interfaces;
using SmartInvetory.DAL.Repository;
namespace SmartInvetory.DAL;
public class DataAccessFactory
{
    private readonly SmartInventoryDbContext _context;
    public DataAccessFactory(SmartInventoryDbContext context) { _context=context; }
    public IRepository<Product,int> GetProductRepository() => new ProductRepository(_context);
    public IProductRepository GetProductRepositoryWithDetails() => new ProductRepository(_context);
    public ICategoryRepository GetCategoryRepository() => new CategoryRepository(_context);
    public ISupplierRepository GetSupplierRepository() => new SupplierRepository(_context);
    public ICustomerRepository GetCustomerRepository() => new CustomerRepository(_context);
    public IWarehouseRepository GetWarehouseRepository() => new WarehouseRepository(_context);
}
