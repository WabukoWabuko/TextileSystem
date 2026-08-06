using Microsoft.EntityFrameworkCore;
using TextileWarehouseERP.Data;
using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Services;

public class SettingsService : ISettingsService
{
    private readonly AppDbContext _db;

    public SettingsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<string> GetAsync(string key, string defaultValue = "")
    {
        var setting = await _db.Settings.FirstOrDefaultAsync(s => s.Key == key);
        return setting?.Value ?? defaultValue;
    }

    public async Task SetAsync(string key, string value)
    {
        var setting = await _db.Settings.FirstOrDefaultAsync(s => s.Key == key);
        if (setting == null)
        {
            setting = new Setting { Key = key, Value = value };
            _db.Settings.Add(setting);
        }
        else
        {
            setting.Value = value;
        }
        await _db.SaveChangesAsync();
    }

    public async Task<string> GetNextItemCodeAsync()
    {
        var prefix = await GetAsync("ItemCodePrefix", "ITM-");
        var numberStr = await GetAsync("NextItemCode", "10001");
        var number = int.Parse(numberStr);

        var code = $"{prefix}{number:D5}";
        await SetAsync("NextItemCode", (number + 1).ToString());
        return code;
    }

    public async Task<string> GetNextInboundNoAsync()
    {
        var prefix = await GetAsync("InboundPrefix", "INB-");
        var numberStr = await GetAsync("NextInboundNo", "1");
        var number = int.Parse(numberStr);

        var code = $"{prefix}{number:D5}";
        await SetAsync("NextInboundNo", (number + 1).ToString());
        return code;
    }

    public async Task<string> GetNextOutboundNoAsync()
    {
        var prefix = await GetAsync("OutboundPrefix", "OUT-");
        var numberStr = await GetAsync("NextOutboundNo", "1");
        var number = int.Parse(numberStr);

        var code = $"{prefix}{number:D5}";
        await SetAsync("NextOutboundNo", (number + 1).ToString());
        return code;
    }
}