using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class MessageTypeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is MessageType messageType)
            return messageType switch
            {
                MessageType.Error => Brushes.Red,
                MessageType.Success => Brushes.Green,
                _ => DependencyProperty.UnsetValue
            };
        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}