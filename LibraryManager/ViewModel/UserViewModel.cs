using System.ComponentModel;
using LibraryManager.Models;

namespace LibraryManager.ViewModel;

public class UserViewModel : INotifyPropertyChanged
{
    private readonly User _user;

    public UserViewModel(User user)
    {
        _user = user;
    }

    public string Username
    {
        get => _user.Username;
        set
        {
            _user.Username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string FirstName
    {
        get => _user.FirstName;
        set
        {
            _user.FirstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get => _user.LastName;
        set
        {
            _user.LastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string Email
    {
        get => _user.Email;
        set
        {
            _user.Email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Phone
    {
        get => _user.Phone;
        set
        {
            _user.Phone = value;
            OnPropertyChanged(nameof(Phone));
        }
    }

    public Role UserRole
    {
        get => _user.Role;
        set
        {
            _user.Role = value;
            OnPropertyChanged(nameof(UserRole));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}