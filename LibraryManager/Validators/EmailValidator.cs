using System.Text.RegularExpressions;

namespace LibraryManager.Validators;

public class EmailValidator : IValidator<string>
{
    public ValidationResult Validate(string? email)
    {
        if (string.IsNullOrEmpty(email)) return new ValidationResult(false, "Email cannot be empty.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return new ValidationResult(false, "Email is not valid.");

        return new ValidationResult(true);
    }
}