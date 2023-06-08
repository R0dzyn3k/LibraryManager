using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Services;
using LibraryManager.Validators;
using PropertyChanged;

namespace LibraryManager.ViewModel;

[AddINotifyPropertyChangedInterface]
public class LoginViewModel
{
    private readonly IAuthService _authService;
    private readonly RelayCommand _goToRegisterCommand;
    private readonly RelayCommand _loginCommand;
    private readonly PasswordValidator _passwordValidator = new();
    private readonly UsernameValidator _usernameValidator = new();
    private string? _password;

    private string? _username;

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
        _loginCommand = new RelayCommand(Login, CanLogin);
        _goToRegisterCommand = new RelayCommand(GoToRegister);
    }

    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            _loginCommand.RaiseCanExecuteChanged();
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            _loginCommand.RaiseCanExecuteChanged();
        }
    }

    public ICommand LoginCommand => _loginCommand;
    public ICommand GoToRegisterCommand => _goToRegisterCommand;

    public string? ErrorMessage { get; private set; }

    public event EventHandler? LoginSuccess;
    public event Action? NavigateToRegister;

    private bool CanLogin()
    {
        var usernameValidation = _usernameValidator.Validate(Username);
        if (!usernameValidation.IsValid)
        {
            ErrorMessage = usernameValidation.ErrorMessage;
            return false;
        }

        if (string.IsNullOrEmpty(_password))
        {
            ErrorMessage = "Password cannot be empty.";
            return false;
        }

        
        return true;
    }

    private async void Login()
    {
        var success = await _authService.LoginAsync(Username, Password);
        if (success)
        {
            LoginSuccess?.Invoke(this, EventArgs.Empty);
            ErrorMessage = _authService.IsLoggedIn ? "True" : "False";
        }
        else
            ErrorMessage = "Invalid login credentials.";
    }

    private void GoToRegister()
    {
        NavigateToRegister?.Invoke();
    }
}