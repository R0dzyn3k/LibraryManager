namespace LibraryManager.Validators;

public class SummaryValidator : IValidator<string>
{
    public ValidationResult Validate(string? summary)
    {
        if (string.IsNullOrEmpty(summary))
            return new ValidationResult(false, "Summary cannot be empty.");

        return new ValidationResult(true);
    }
}