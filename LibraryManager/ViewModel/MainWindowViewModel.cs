using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Data;
using LibraryManager.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly LibraryDbContext _dbContext;
    
    public LoginViewModel LoginViewModel { get; set; }

    public RegisterViewModel RegisterViewModel { get; set; }

    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public RelayCommand RegisterCommand { get; private set; }

    public MainWindowViewModel()
    {
        _dbContext = new LibraryDbContext();
        _dbContext.Database.Migrate();

        var authService = new AuthService(_dbContext);

        LoginViewModel = new LoginViewModel(authService);
        LoginViewModel.LoginSuccess += OnLoginSuccess;

        RegisterViewModel = new RegisterViewModel();

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
    

}