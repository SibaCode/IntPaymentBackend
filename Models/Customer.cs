namespace IntPaymentAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IDNumber { get; set; } // National ID number
        public string AccountNumber { get; set; }
        public string PasswordHash { get; set; } 
        public ICollection<Transaction> Transactions { get; set; }
// Ensure this is hashed and salted in production
    }
}
