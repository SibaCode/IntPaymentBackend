using System;
using System.ComponentModel.DataAnnotations;

namespace IntPaymentAPI.Models
{
   public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Provider { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }

    public Customer Customer { get; set; }
}
}
