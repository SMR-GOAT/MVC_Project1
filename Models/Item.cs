using System.ComponentModel.DataAnnotations;

namespace MVCCourse.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public string? Description { get; set; } // وصف المنتج

    [Range(0.1, 100000)]
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; } // رابط صورة المنتج

    public int StockQuantity { get; set; } // الكمية المتوفرة في المخزن

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // ربط المنتج بالفئة (اختياري مستقبلاً)
    // public int CategoryId { get; set; }
}