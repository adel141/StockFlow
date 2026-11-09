using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.Interfaces;
namespace SmartInvetory.DAL.Repository;
public class Repository<CLASS, ID> : IRepository<CLASS, ID> where CLASS : class
{
    protected readonly SmartInventoryDbContext _context;
    protected readonly DbSet<CLASS> _dbSet;
    public Repository(SmartInventoryDbContext context) { _context=context; _dbSet=context.Set<CLASS>(); }
    public virtual bool Add(CLASS entity) { _dbSet.Add(entity); return _context.SaveChanges()>0; }
    public virtual bool Update(CLASS entity) { _dbSet.Update(entity); return _context.SaveChanges()>0; }
    public virtual bool Delete(ID id) { var entity=_dbSet.Find(id); if(entity==null)return false; _dbSet.Remove(entity); return _context.SaveChanges()>0; }
    public virtual List<CLASS> GetAll() => _dbSet.AsNoTracking().ToList();
    public virtual CLASS GetByID(ID id) => _dbSet.Find(id)!;
}
