using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Data;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.View;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private AuthService _authService;
    private readonly LibraryDbContext _dbContext;
    private object _currentView;

    public RelayCommand LogoutCommand { get; private set; }

    public RelayCommand MyAccountCommand { get; private set; }

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public LoginViewModel LoginViewModel { get; set; }

    public RegisterViewModel RegisterViewModel { get; set; }

    public RelayCommand RegisterCommand { get; private set; }

    private bool _isLoggedIn;

    public bool IsLoggedIn
    {
        get { return _isLoggedIn; }
        private set
        {
            _isLoggedIn = value;
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }
    
    private bool _isAdmin;

    public bool IsAdmin
    {
        get { return _isAdmin; }
        private set
        {
            _isAdmin = value;
            OnPropertyChanged(nameof(IsAdmin));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainWindowViewModel()
    {
        _dbContext = new LibraryDbContext();
        _dbContext.Database.Migrate();

        _authService = new AuthService(_dbContext);
        _authService.PropertyChanged += AuthServiceOnPropertyChanged;
        
        LogoutCommand = new RelayCommand(Logout);
        MyAccountCommand = new RelayCommand(MyAccount);

        LoginViewModel = new LoginViewModel(_authService);
        LoginViewModel.LoginSuccess += OnLoginSuccess;
        LoginViewModel.NavigateToRegister += OnNavigateToRegister;

        RegisterViewModel = new RegisterViewModel(_authService);
        RegisterViewModel.NavigateToLogin += OnNavigateToLogin;

        RegisterCommand = new RelayCommand(() => CurrentView = RegisterViewModel);

        CurrentView = LoginViewModel;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnLoginSuccess(object sender, EventArgs e)
    {
        // Przełącz na inną stronę po pomyślnym zalogowaniu
        // CurrentView = new SomeOtherViewModel();
    }

    private void OnNavigateToRegister()
    {
        CurrentView = RegisterViewModel;
    }

    private void OnNavigateToLogin()
    {
        CurrentView = LoginViewModel;
    }

    private void Logout()
    {
        _authService.Logout();
        CurrentView = LoginViewModel;
    }
    
    private void AuthServiceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthService.IsLoggedIn))
        {
            IsLoggedIn = _authService.IsLoggedIn;
            OnPropertyChanged(nameof(IsLoggedIn));
            UpdateIsAdmin();
        }
    }
    
    private void UpdateIsAdmin()
    {
        IsAdmin = _authService.UserRole == Role.Admin;
    }
    
    private void MyAccount()
    {
        User? user = _authService.GetCurrentUser();
        
        if (IsLoggedIn && user != null)
        {
            UserView userView = new UserView();
            userView.DataContext = user;
            CurrentView = userView;
        }
        else
        {
            CurrentView = LoginViewModel;
        }
    }
}