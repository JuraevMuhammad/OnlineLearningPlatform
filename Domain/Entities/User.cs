using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [StringLength(50)]
    public required string FirstName { get; set; }
    [StringLength(50)] 
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    [Phone]
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public required string Email { get; set; } 
    [Required]
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public DateTime UpdatedAt { get; set; }
}