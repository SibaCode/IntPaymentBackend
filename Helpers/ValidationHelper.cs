using System.Text.RegularExpressions;
public static class ValidationHelper
{
    public static bool IsValidAccountNumber(string accountNumber)
    {
        var regex = new Regex(@"^[a-zA-Z0-9\-]+$");
        return regex.IsMatch(accountNumber);
    }

    public static bool IsValidSWIFTCode(string swiftCode)
    {
        var regex = new Regex(@"^[A-Za-z]{4}[A-Za-z]{2}[0-9A-Za-z]{2}[A-Za-z0-9]{3}$");
        return regex.IsMatch(swiftCode);
    }
}
