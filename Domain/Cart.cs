using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;


public class Cart
{
    [Key]
    [Required]
    public long Id { get; set; }
    [Required]
    public User User { get; set; }
    [Required]
    public long UserId { get; set; }

    public List<CartItem> CartItems { get; set; }
}

