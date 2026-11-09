using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.EF.Tables;
using SmartInvetory.DAL.Interfaces;
namespace SmartInvetory.DAL.Repository;
public class CategoryRepository : Repository<Category, int>, ICategoryRepository
{
    public CategoryRepository(SmartInventoryDbContext context) : base(context) { }
    public override List<Category> GetAll() => _context.Categories.Include(x => x.Products).AsNoTracking().ToList();
}
