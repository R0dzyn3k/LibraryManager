using System.ComponentModel;
using LibraryManager.Services;

namespace LibraryManager.ViewModel;

public class DashboardViewModel : INotifyPropertyChanged
{
    private string _userName;

    public DashboardViewModel(IAuthService authService)
    {
        var currentUser = authService.GetCurrentUser();
        if (currentUser != null) UserName = currentUser.Username;
    }

    public string UserName
    {
        get => _userName;
        set
        {
            if (_userName != value)
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}