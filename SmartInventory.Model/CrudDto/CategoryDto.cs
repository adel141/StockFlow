using System.ComponentModel.DataAnnotations;
namespace SmartInventory.Model.CrudDto;
public class CategoryDto { public int CategoryId { get; set; } [Required, StringLength(100)] public string CategoryName { get; set; } = ""; [StringLength(250)] public string? Description { get; set; } public bool IsActive { get; set; } public int ProductCount { get; set; } }
