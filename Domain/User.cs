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
    [Required]
    public required long CartId { get; set; }
    [Required]
    public required Cart Cart { get; set; }

}


public enum Role
{
    User,
    Admin
}