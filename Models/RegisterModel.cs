using System.ComponentModel.DataAnnotations;

namespace IntPaymentAPI.Models
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z\s]{2,50}$", ErrorMessage = "Full Name must be letters and spaces only.")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{6,20}$", ErrorMessage = "Account Number must be digits only.")]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{6,20}$", ErrorMessage = "ID Number must be digits only.")]
        public string IDNumber { get; set; }
    }
}
