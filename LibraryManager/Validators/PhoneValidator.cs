using System.Text.RegularExpressions;

namespace LibraryManager.Validators;

public class PhoneValidator : IValidator<string>
{
    public ValidationResult Validate(string? phone)
    {
        if (string.IsNullOrEmpty(phone)) return new ValidationResult(false, "Phone number cannot be empty.");

        if (phone.Length is < 8 or > 12) return new ValidationResult(false, "Phone should contain 8 to 12 characters");

        if (!Regex.IsMatch(phone, @"[0-9]+"))
            return new ValidationResult(false, "Password should contain only numbers.");


        return new ValidationResult(true);
    }
}