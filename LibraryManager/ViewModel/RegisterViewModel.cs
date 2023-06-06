using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.Validators;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace LibraryManager.ViewModel;

[AddINotifyPropertyChangedInterface]
public class RegisterViewModel
{
    private readonly IAuthService _authService;
    private readonly RelayCommand _registerCommand;
    private readonly RelayCommand _goToLoginCommand;
    private readonly UsernameValidator _usernameValidator = new();
    private readonly FirstNameValidator _firstNameValidator = new();
    private readonly LastNameValidator _lastNameValidator = new();
    private readonly EmailValidator _emailValidator = new();
    private readonly PhoneValidator _phoneValidator = new();
    private readonly PasswordValidator _passwordValidator = new();


    public event Action? NavigateToLogin;
    private string? _username;
    private string? _password;
    private string? _firstName;
    private string? _lastName;
    private string? _email;
    private string? _phone;

    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            _registerCommand.RaiseCanExecuteChanged();
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            _registerCommand.RaiseCanExecuteChanged();
        }
    }

    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            _registerCommand.RaiseCanExecuteChanged();
        }
    }

    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            _registerCommand.RaiseCanExecuteChanged();
        }
    }

    public string? Email
    {
        get => _email;
        set
        {
            _email = value;
            _registerCommand.RaiseCanExecuteChanged();
        }
    }

    public string? Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            _registerCommand.RaiseCanExecuteChanged();
        }
    }

    public ICommand RegisterCommand => _registerCommand;
    public ICommand GoToLoginCommand => _goToLoginCommand;

    public string? ErrorMessage { get; private set; }

    public RegisterViewModel(IAuthService authService)
    {
        _authService = authService;
        _registerCommand = new RelayCommand(Register, CanRegister);
        _goToLoginCommand = new RelayCommand(GoToLogin);
    }

    private void Register()
    {
        var canRegister = CanRegister();
        if (!canRegister)
            return;
        
    
        var newUser = new User(Username, Password, FirstName, LastName, Email, Phone);

        Task.Run(() => _authService.RegisterAsync(newUser))
            .ContinueWith(task =>
            {
                if (task.Result)
                {
                    NavigateToLogin?.Invoke();
                }
                else
                {
                    ErrorMessage = "Failed to register user.";
                }
            });
    }

    private bool CanRegister()
    {
        var usernameValidation = _usernameValidator.Validate(Password);
        if (!usernameValidation.IsValid)
        {
            ErrorMessage = usernameValidation.ErrorMessage;
            return false;
        }
        
        var passwordValidation = _passwordValidator.Validate(Password);
        if (!passwordValidation.IsValid)
        {
            ErrorMessage = passwordValidation.ErrorMessage;
            return false;
        }

        var firstNameValidation = _firstNameValidator.Validate(FirstName);
        if (!firstNameValidation.IsValid)
        {
            ErrorMessage = firstNameValidation.ErrorMessage;
            return false;
        }

        var lastNameValidation = _lastNameValidator.Validate(LastName);
        if (!lastNameValidation.IsValid)
        {
            ErrorMessage = lastNameValidation.ErrorMessage;
            return false;
        }

        var emailValidation = _emailValidator.Validate(Email);
        if (!emailValidation.IsValid)
        {
            ErrorMessage = emailValidation.ErrorMessage;
            return false;
        }

        var phoneValidation = _phoneValidator.Validate(Phone);
        if (!phoneValidation.IsValid)
        {
            ErrorMessage = phoneValidation.ErrorMessage;
            return false;
        }

        ErrorMessage = null;
        return true;
    }
    
    private void GoToLogin()
    {
        NavigateToLogin?.Invoke();
    }
}
