using SmartInvetory.DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;
namespace SmartInvetory.DAL.Interfaces
{
    public interface IProductRepository : IRepository<Product, int>
    {
        // Product retrieval
        Product GetProductDetails(int id);
        // Product search
        List<Product> GetProductsByName(string name);
        // Product filtering
        List<Product> GetProductsBySku(string sku);
        List<Product> GetProductsByBarcode(string barcode);
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetActiveProducts();
        List<Product> GetInactiveProducts();
        List<Product> GetLowStockProducts();
        // Product validation
        bool IsSkuExists(string sku, int? excludeProductId = null);
        bool IsBarcodeExists(string barcode, int? excludeProductId = null);
    }
}
