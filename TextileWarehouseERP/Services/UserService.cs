using Microsoft.EntityFrameworkCore;
using TextileWarehouseERP.Data;
using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly IAuditService _auditService;

    public User? CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;
    public string CurrentRole => CurrentUser?.Role ?? string.Empty;

    public UserService(AppDbContext db, IAuditService auditService)
    {
        _db = db;
        _auditService = auditService;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var hash = DbInitializer.HashPassword(password);

        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash && u.IsActive);

        if (user == null)
        {
            await _auditService.LogAsync("LOGIN_FAILED", $"Failed login attempt for: {username}");
            return null;
        }

        user.LastLogin = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        CurrentUser = user;
        await _auditService.LogAsync("LOGIN", "User logged in successfully");

        return user;
    }

    public async Task LogoutAsync()
    {
        if (CurrentUser != null)
        {
            await _auditService.LogAsync("LOGOUT", "User logged out");
            CurrentUser = null;
        }
    }
}