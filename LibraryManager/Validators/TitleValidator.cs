namespace LibraryManager.Validators;

public class TitleValidator : IValidator<string>
{
    public ValidationResult Validate(string? title)
    {
        if (string.IsNullOrEmpty(title))
            return new ValidationResult(false, "Title cannot be empty.");

        return new ValidationResult(true);
    }
}