using System;
using System.ComponentModel.DataAnnotations;
public class TransactionDetails
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Provider { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
}
