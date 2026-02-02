using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParrotShopBackend.Domain;



public class CartItem
{
    [Key]
    public long Id { get; set; }
    public long CartId { get; set; }
    public long ItemId { get; set; }
    public required Item Item { get; set; }

    [Range(1, 100)]
    public int Qty { get; set; }
}