using System.Threading.Tasks;

namespace LibraryManager.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string? username, string? password);
    void Logout();
}