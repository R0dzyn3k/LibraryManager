namespace LibraryManager.Validators;

public class PublisherValidator : IValidator<string>
{
    public ValidationResult Validate(string? publisher)
    {
        if (string.IsNullOrEmpty(publisher))
            return new ValidationResult(false, "Publisher cannot be empty.");

        return new ValidationResult(true);
    }
}