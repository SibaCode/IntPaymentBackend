using System.ComponentModel.DataAnnotations;
namespace IntPaymentAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; } // Ensure you hash and salt the password before saving
    }
}
