public class LoginResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string AccountNumber { get; set; }
    public string Token { get; set; } // Optional: If you're using JWT
}
