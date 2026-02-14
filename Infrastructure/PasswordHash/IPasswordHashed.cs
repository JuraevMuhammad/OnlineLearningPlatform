namespace Infrastructure.PasswordHash;

public interface IPasswordHashed
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string password, string hashedPassword);
}