using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;



public class Order
{
    [Key]
    public long Id { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public User User { get; set; }
    [Required]
    public DateTime Timestamp { set; get; }
    [Required]
    public string ShippingAddress { get; set; } = string.Empty;

    public List<OrderItem> OrderItems { get; set; } = new();

}