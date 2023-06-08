namespace LibraryManager.Validators;

public class TotalCountValidator : IValidator<int?>
{
    public ValidationResult Validate(int? totalCount)
    {
        if (totalCount == null)
            return new ValidationResult(false, "Total count cannot be null.");

        if (totalCount < 0)
            return new ValidationResult(false, "Total count cannot be negative.");

        return new ValidationResult(true);
    }
}