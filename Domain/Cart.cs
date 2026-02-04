using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;


public class Cart
{
    [Key]
    [Required]
    public required long Id { get; set; }
    [Required]
    public required User User { get; set; }
    [Required]
    public required long UserId { get; set; }

    public required List<CartItem> CartItems { get; set; }
}

