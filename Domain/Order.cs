using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;



public class Order
{
    [Key]
    public long Id { get; set; }
    [Required]
    public required long UserId { get; set; }
    [Required]
    public required User User { get; set; }
    [Required]
    public required DateTime Timestamp { set; get; }
    [Required]
    public required string ShippingAddress { get; set; } = string.Empty;

    public List<OrderItem> OrderItems { get; set; } = new();

}