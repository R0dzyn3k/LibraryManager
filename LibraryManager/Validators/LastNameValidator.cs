using System.Text.RegularExpressions;

namespace LibraryManager.Validators;

public class LastNameValidator : IValidator<string>
{
    public ValidationResult Validate(string lastName)
    {
        if (string.IsNullOrEmpty(lastName))
        {
            return new ValidationResult(false, "Last name cannot be empty.");
        }
        
        if (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
        {
            return new ValidationResult(false, "Last name should contain only letters.");
        }
        
        return new ValidationResult(true);
    } 
}