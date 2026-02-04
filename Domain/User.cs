using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;


public class User
{
    [Required]
    [Key]
    public required long Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    [Required]
    [StringLength(100)]
    public required string Username { get; set; }
    [Required]
    public required string PasswordHash { get; set; }
    [StringLength(100)]
    public string? Email { get; set; }
    [Required]
    public required Role Role { get; set; }
    public Cart? Cart { get; set; } //we don't need a cart for user to exist
    public List<Order> Orders { get; set; }


}


public enum Role
{
    User,
    Admin
}