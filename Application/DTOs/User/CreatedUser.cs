using Domain.Enum;

namespace Application.DTOs.User;

public class CreatedUser
{
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserRole Role { get; set; }
}