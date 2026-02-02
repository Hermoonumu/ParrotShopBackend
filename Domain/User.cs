using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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
    public required Role role { get; set; }
    [Required]
    public required long cartId { get; set; }
    [Required]
    public required Cart cart { get; set; }

}


public enum Role
{
    User,
    Admin
}