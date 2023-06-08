using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class UserRoleToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Role userRole && Enum.TryParse(parameter?.ToString(), out Role requiredRole))
            return userRole == requiredRole ? Visibility.Visible : Visibility.Collapsed;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}