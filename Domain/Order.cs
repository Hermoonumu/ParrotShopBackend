using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;



public class Order
{
    [Key]
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public DateTime Timestamp { set; get; }
    public string ShippingAddress { get; set; } = string.Empty;

    public List<OrderItem> OrderItems { get; set; } = new();

}