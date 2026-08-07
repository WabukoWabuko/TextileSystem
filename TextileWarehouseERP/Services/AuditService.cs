using System;
using System.Threading.Tasks;
using TextileWarehouseERP.Data;
using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Services;

public class AuditService : IAuditService
{
    private readonly AppDbContext _db;
    private readonly IUserService? _userService;

    public AuditService(AppDbContext db, IUserService? userService = null)
    {
        _db = db;
        _userService = userService;
    }

    public async Task LogAsync(string action, string details)
    {
        var log = new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            Username = _userService?.CurrentUser?.Username ?? "SYSTEM",
            Role = _userService?.CurrentUser?.Role ?? "SYSTEM",
            Action = action,
            Details = details,
            ComputerName = Environment.MachineName
        };

        _db.AuditLogs.Add(log);
        await _db.SaveChangesAsync();
    }
}