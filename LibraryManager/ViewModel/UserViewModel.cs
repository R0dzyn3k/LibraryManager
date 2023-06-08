using System.ComponentModel;
using LibraryManager.Models;

namespace LibraryManager.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User _user;

        public UserViewModel(User user)
        {
            _user = user;
        }

        public string Username
        {
            get { return _user.Username; }
            set
            {
                _user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string FirstName
        {
            get { return _user.FirstName; }
            set
            {
                _user.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _user.LastName; }
            set
            {
                _user.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get { return _user.Email; }
            set
            {
                _user.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get { return _user.Phone; }
            set
            {
                _user.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public Role UserRole
        {
            get { return _user.Role; }
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
}