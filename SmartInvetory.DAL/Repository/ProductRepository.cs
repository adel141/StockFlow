using Microsoft.EntityFrameworkCore;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.EF.Tables;
using SmartInvetory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace SmartInvetory.DAL.Repository
{
    public class ProductRepository : Repository<Product, int>,IProductRepository
    {
        private readonly SmartInventoryDbContext _context;
        public ProductRepository(SmartInventoryDbContext context) : base(context)
        {
            _context = context;
            
        }
        
        
        public List<Product> GetActiveProducts()
        {
            return _context.Products.Where(p => p.IsActive).ToList();
        }
        public List<Product> GetInactiveProducts()
        {
            return _context.Products.Where(p => !p.IsActive).ToList();
        }
        public List<Product> GetLowStockProducts()
        {
            return _context.Products
                .Include(p=>p.StockBalances)
                .Where(p=>
                p.StockBalances.Sum(sb => sb.Quantity) <= p.ReorderLevel)
                .ToList();
        }
        public Product GetProductDetails(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.StockBalances)
                    .ThenInclude(sb => sb.Warehouse)
                .FirstOrDefault(p => p.ProductId == id);  
        }
        public List<Product> GetProductsByBarcode(string barcode)
        {
            return _context.Products
                .Where(p => p.Barcode == barcode)
                .ToList();
        }
        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        
        }
        public List<Product> GetProductsByName(string name)
        {
            return _context.Products
                .Where(p => p.ProductName.Contains(name))
                .ToList();
        }
        public List<Product> GetProductsBySku(string sku)
        {
            return _context.Products
                .Where(p => p.Sku == sku)
                .ToList();
        }
        public bool IsBarcodeExists(string barcode, int? excludeProductId = null)
        {
            if (string.IsNullOrWhiteSpace(barcode)) {
                return false;
            }
            return _context.Products.Any(p =>
                p.Barcode == barcode &&
                (!excludeProductId.HasValue ||
                 p.ProductId != excludeProductId.Value));
        }
        public bool IsSkuExists(string sku, int? excludeProductId = null)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                return false;
            }
            return _context.Products.Any(p =>
                p.Sku == sku &&
                (!excludeProductId.HasValue ||
                 p.ProductId != excludeProductId.Value));
        }
        public  bool Add(Product entity)
        {
            if (IsSkuExists(entity.Sku))
            {
                throw new InvalidOperationException($"A product with SKU '{entity.Sku}' already exists.");
            }
            if (!string.IsNullOrWhiteSpace(entity.Barcode) && IsBarcodeExists(entity.Barcode))
            {
                throw new InvalidOperationException($"A product with Barcode '{entity.Barcode}' already exists.");
            }
            return base.Add(entity);
        }
        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
    }
}
