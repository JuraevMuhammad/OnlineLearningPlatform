namespace Application.DTOs.User;

public class GetUser
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}