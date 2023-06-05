namespace LibraryManager.Services;

public interface INavigationService
{
    void NavigateTo(string viewName);
    void GoBack();
}