using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;



public class Order
{
    [Key]
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }

}