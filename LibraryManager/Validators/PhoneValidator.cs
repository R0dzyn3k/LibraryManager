using System.Text.RegularExpressions;

namespace LibraryManager.Validators;

public class PhoneValidator
{
    public ValidationResult Validate(string phone)
    {
        if (string.IsNullOrEmpty(phone))
        {
            return new ValidationResult(false, "Phone number cannot be empty.");
        }

        // (\\+\\s)? - group that consists of '+' and space. The group is optional due to ?
        // \\d{9,} - at least 9 digits
        if (!Regex.IsMatch(phone, "^(\\+\\s)?\\d{9,}$"))
        {
            return new ValidationResult(false, "Phone number is not valid. At least 9 digits and optional \"+ \".");
        }
        
        return new ValidationResult(true);
    }
}