using System.ComponentModel.DataAnnotations;

namespace IntPaymentAPI.Models
{
    public class CustomerRegisterDto
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name must contain letters and spaces only.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Account Number is required1.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Account Number must contain digits only.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "ID Number is required.")]
        [RegularExpression(@"^\d{6,}$", ErrorMessage = "ID Number must be at least 6 digits.")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must contain at least one letter and one number.")]
        public string Password { get; set; }
    }
}
