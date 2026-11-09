using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartInvetory.DAL.Repository
{
    internal class Repository<CLASS, ID> : IRepository<CLASS, ID> where CLASS : class
    {
        private readonly SmartInventoryDbContext _context;
        private readonly DbSet<CLASS> _dbSet;
        public Repository(SmartInventoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<CLASS>();
        }

        public bool Add(CLASS entity)
        {
            _dbSet.Add(entity);
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
        public bool Delete(ID id)
        {
            CLASS entity = _dbSet.Find(id);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
        public List<CLASS> GetAll()
        {
            return _dbSet.ToList();
        }
        public CLASS GetByID(ID id)
        {
            return _dbSet.Find(id);
        }
        public bool Update(CLASS entity)
        {
            _dbSet.Update(entity);
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
