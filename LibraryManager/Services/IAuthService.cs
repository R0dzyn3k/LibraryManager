using System.Threading.Tasks;
using LibraryManager.Models;

namespace LibraryManager.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string? username, string? password);
    Task<bool> RegisterAsync(User newUser);
    Task<bool> CheckUserExistsAsync(string? username);
    void Logout();
}