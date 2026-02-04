using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;

public class RevokedJWT
{
    [Key]
    public required string Token { get; set; }
    [Required]
    public long UserID { set; get; }
    [Required]
    public required User User { set; get; }

}