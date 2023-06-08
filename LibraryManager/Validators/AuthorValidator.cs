namespace LibraryManager.Validators;

public class AuthorValidator : IValidator<string>
{
    public ValidationResult Validate(string? author)
    {
        if (string.IsNullOrEmpty(author))
            return new ValidationResult(false, "Author cannot be empty.");

        return new ValidationResult(true);
    }
}