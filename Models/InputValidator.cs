using System.Text.RegularExpressions;

public class InputValidator
{
    private bool ValidateInput(string input, string pattern)
    {
        var regex = new Regex(pattern);
        return regex.IsMatch(input);
    }
       public bool ValidatePassword(string password)
    {
        var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
        return ValidateInput(password, passwordPattern);
    }

}