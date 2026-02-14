namespace Infrastructure.PasswordHash;

public class PasswordHashed : IPasswordHashed
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool VerifyHashedPassword(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}