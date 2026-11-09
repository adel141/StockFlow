using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SmartInventory.Model.ProductDto
{
    public class ProductCreateDto
    {
        [Required, StringLength(150)] public string ProductName { get; set; } = "";
        [Required, StringLength(50)] public string Sku { get; set; } = "";
        [StringLength(100)] public string? Barcode { get; set; }
        public int CategoryId { get; set; }
        [StringLength(500)] public string? Description { get; set; }
        [Required, StringLength(30)] public string Unit { get; set; } = "Piece";
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        [Range(0, int.MaxValue)] public int ReorderLevel { get; set; }
    }
}
