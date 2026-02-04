using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParrotShopBackend.Domain;


public class Item
{
    [Key]
    public long Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }

}