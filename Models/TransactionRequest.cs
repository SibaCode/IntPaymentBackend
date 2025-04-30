namespace IntPaymentAPI.Models
{
    public class TransactionRequest
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string SWIFTCode { get; set; }
        public string Provider { get; set; }
    }
}
