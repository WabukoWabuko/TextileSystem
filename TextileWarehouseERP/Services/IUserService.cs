using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Services;

public interface IUserService
{
    Task<User?> LoginAsync(string username, string password);
    Task LogoutAsync();
    User? CurrentUser { get; }
    bool IsLoggedIn { get; }
    string CurrentRole { get; }
}