using BCrypt.Net;

public class UserService
{
    public string HashPassword(string password)
{
    try
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }
    catch (Exception ex)
    {
        // Log the exception or handle it in a way that makes sense for your application
        throw new Exception("Error hashing password", ex);
    }
}

public bool VerifyPassword(string password, string hashedPassword)
{
    try
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
    catch (Exception ex)
    {
        // Log the exception or handle it in a way that makes sense for your application
        throw new Exception("Error verifying password", ex);
    }
}
}