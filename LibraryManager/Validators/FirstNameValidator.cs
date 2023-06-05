using System.Text.RegularExpressions;

namespace LibraryManager.Validators;

public class FirstNameValidator : IValidator<string>
{
    public ValidationResult Validate(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            return new ValidationResult(false, "First name cannot be empty.");
        }
        
        if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
        {
            return new ValidationResult(false, "First name should contain only letters.");
        }
        
        return new ValidationResult(true);
    } 
}