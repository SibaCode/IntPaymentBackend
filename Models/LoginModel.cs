using System.ComponentModel.DataAnnotations;

namespace IntPaymentAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[A-Za-z\s]{2,50}$", ErrorMessage = "Full Name must be letters and spaces only.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Account Number is required.")]
        [RegularExpression(@"^[0-9]{6,20}$", ErrorMessage = "Account Number must be digits only.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
         [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*#?&]{6,}$", 
            ErrorMessage = "Password must be at least 6 characters and include letters and numbers.")]
        public string Password { get; set; }
    }
}
