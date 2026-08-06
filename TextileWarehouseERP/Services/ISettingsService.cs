namespace TextileWarehouseERP.Services;

public interface ISettingsService
{
    Task<string> GetAsync(string key, string defaultValue = "");
    Task SetAsync(string key, string value);
    Task<string> GetNextItemCodeAsync();
    Task<string> GetNextInboundNoAsync();
    Task<string> GetNextOutboundNoAsync();
}