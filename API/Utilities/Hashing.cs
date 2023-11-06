using BCrypt.Net;

namespace API.Utilities;

public class Hashing
{
    public static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12);
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
    }

    public static bool ValidatePassword(string password, string HashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, HashPassword);
    }
}
