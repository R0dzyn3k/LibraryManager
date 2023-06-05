using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManager.Data;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly LibraryDbContext _context;
        private List<User>? _users;
        private User? _currentUser;

        public AuthService(LibraryDbContext context)
        {
            _context = context;
            InitializeUsers();
        }

        private async void InitializeUsers()
        {
            _users = await _context.Users.AsNoTracking().ToListAsync();
        }

        public Task<bool> LoginAsync(string? username, string? password)
        {
            var user = _users?.FirstOrDefault(u => u.Username == username && u.Password == password);
            
            if (user == null)
                return Task.FromResult(false);
            
            _currentUser = user;
            
            return Task.FromResult(true);
        }

        public void Logout()
        {
            _currentUser = null;
        }
    }
}