using System.Text.RegularExpressions;

namespace LibraryManager.Validators;

public class PasswordValidator : IValidator<string>
{
    public ValidationResult Validate(string? password)
    {
        if (string.IsNullOrEmpty(password)) return new ValidationResult(false, "Password cannot be empty.");

        if (password.Length < 5) return new ValidationResult(false, "Password should contain at least 5 characters.");

        if (!Regex.IsMatch(password, @"[A-Z]+"))
            return new ValidationResult(false, "Password should contain at least one upper case letter.");

        if (!Regex.IsMatch(password, @"[a-z]+"))
            return new ValidationResult(false, "Password should contain at least one lower case letter.");

        if (!Regex.IsMatch(password, @"[0-9]+"))
            return new ValidationResult(false, "Password should contain at least one digit.");

        return new ValidationResult(true);
    }
}