public class PaymentDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Provider { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; } // To associate the payment with a customer
}
