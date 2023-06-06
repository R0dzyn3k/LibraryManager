using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManager.Data;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Services;

public class AuthService : IAuthService
{
    private readonly LibraryDbContext _context;
    private User? _currentUser;
    private List<User>? _users;

    public AuthService(LibraryDbContext context)
    {
        _context = context;
        InitializeUsers();
    }

    public Task<bool> LoginAsync(string? username, string? password)
    {
        var user = _users?.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user == null)
            return Task.FromResult(false);

        _currentUser = user;

        return Task.FromResult(true);
    }

    public async Task<bool> RegisterAsync(User newUser)
    {
        if (_context.Users == null)
            return false;
        
        try
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public async Task<bool> CheckUserExistsAsync(string? username)
    {
        if (username == null)
            return false;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        return user != null;
    }
    
    public void Logout()
    {
        _currentUser = null;
    }

    private async void InitializeUsers()
    {
        if (_context.Users != null)
            _users = await _context.Users.AsNoTracking().ToListAsync();
    }
}