using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartInventory.Model.ProductDto
{
    public class ProductListModel
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ReorderLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
