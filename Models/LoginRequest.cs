namespace IntPaymentAPI.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
    }
}
