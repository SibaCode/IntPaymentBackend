namespace IntPaymentAPI.Models
{
   public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Provider { get; set; }
    public string Status { get; set; } = "Pending";
    public string AccountNumber { get; set; }
    public string SwiftCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Key to Customer
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } // Navigation Property
}

}
