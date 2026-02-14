namespace Application.DTOs.User;

public class UpdateUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; } 
    public string? LastPassword { get; set; }
    public string? NewPassword { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}