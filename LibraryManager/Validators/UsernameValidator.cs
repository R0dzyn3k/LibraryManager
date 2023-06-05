namespace LibraryManager.Validators;

public class UsernameValidator : IValidator<string>
{
    public ValidationResult Validate(string? username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return new ValidationResult(false, "Username cannot be empty.");
        }

        if (username.Length < 3)
        {
            return new ValidationResult(false, "Username should contain at least 3 characters.");
        }

        // TODO dodać sprawdzenie unikalności

        return new ValidationResult(true);
    }
}