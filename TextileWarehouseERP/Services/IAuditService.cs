namespace TextileWarehouseERP.Services;

public interface IAuditService
{
    Task LogAsync(string action, string details);
}