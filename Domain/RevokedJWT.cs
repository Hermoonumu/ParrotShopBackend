using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;

public class RevokedJWT
{
    [Key]
    public string Token { get; set; }
    [Required]
    public long UserID { set; get; }
    [Required]
    public User User { set; get; }

}