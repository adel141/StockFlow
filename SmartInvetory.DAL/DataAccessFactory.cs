using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.EF.Tables;
using SmartInvetory.DAL.Interfaces;
using SmartInvetory.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartInvetory.DAL
{
    public class DataAccessFactory
    {
        SmartInventoryDbContext _context;
        public DataAccessFactory(SmartInventoryDbContext context)
        {
            _context = context;
        }
        public IRepository<Product, int> GetProductRepository()
        {
            return new ProductRepository(_context);
        }
        public IProductRepository GetProductRepositoryWithDetails()
        {
            return new ProductRepository(_context);
        }

    }
}
