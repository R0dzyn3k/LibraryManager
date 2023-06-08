namespace LibraryManager.Validators;

public class CountValidator : IValidator<int>
{
    public ValidationResult Validate(int count)
    {
        if (count < 0)
            return new ValidationResult(false, "Count cannot be negative.");

        return new ValidationResult(true);
    }
}