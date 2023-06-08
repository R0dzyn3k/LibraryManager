namespace LibraryManager.Validators;

public class GenreValidator : IValidator<string>
{
    public ValidationResult Validate(string? genre)
    {
        if (string.IsNullOrEmpty(genre))
            return new ValidationResult(false, "Genre cannot be empty.");

        return new ValidationResult(true);
    }
}