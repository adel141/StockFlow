using System;
using System.Collections.Generic;
using System.Text;

namespace SmartInventory.Model.ProductDto
{
    public class ProductDetailsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string Description { get; set; }
        public string Unit { get; set; }

        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int ReorderLevel { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Overall stock information
        public int TotalQuantity { get; set; }
        public bool IsLowStock { get; set; }

        // Warehouse-wise stock
        public List<WarehouseStockDto> WarehouseStocks { get; set; }
    }

    public class WarehouseStockDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
