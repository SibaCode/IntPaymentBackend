public class TransactionDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Provider { get; set; }
    public string SwiftCode { get; set; }
    public int CustomerId { get; set; }  
        public string AccountNumber { get; set; }  // AccountNumber field

    // Foreign Key
}
