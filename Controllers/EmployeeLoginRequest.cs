using System.ComponentModel.DataAnnotations;
namespace IntPaymentAPI.Models
{
    public class EmployeeLoginRequest
    {
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9_]{4,20}$", ErrorMessage = "Username must be 4–20 characters, letters/numbers/underscores only.")]
    public string Username { get; set; }

    [Required]
    [RegularExpression(@"^[^\s]{6,30}$", ErrorMessage = "Password must be 6–30 characters with no spaces.")]
    public string Password { get; set; }
        
       
    }
}
