using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Services;

public class NavigationService : INavigationService
{
    private readonly Dictionary<string, Uri> views = new();

    public NavigationService()
    {
        views.Add("Dashboard", new Uri("../Views/DashboardView.xaml", UriKind.Relative));
        views.Add("Login", new Uri("../Views/LoginView.xaml", UriKind.Relative));
    }

    public void NavigateTo(string viewName)
    {
        // Pobierz główne okno aplikacji
        var mainWindow = Application.Current.MainWindow;

        // Znajdź Uri dla widoku
        if (views.TryGetValue(viewName, out var uri))
            // Naviguj do widoku
            (mainWindow.Content as Frame).Navigate(uri);
    }

    public void GoBack()
    {
        // Pobierz główne okno aplikacji
        var mainWindow = Application.Current.MainWindow;

        // Cofnij do poprzedniego widoku
        (mainWindow.Content as Frame).GoBack();
    }
}