using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;



public class RefreshToken
{
    [Key]
    public required string Token { get; set; }
    [Required]
    public long UserID { set; get; }
    [Required]
    public required User User { set; get; }
    [Required]
    public DateTime IssuedAt { set; get; }
    [Required]
    public DateTime ExpiresAt { set; get; }
}