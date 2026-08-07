using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TextileWarehouseERP.Data;
using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Services;

public class ItemService : IItemService
{
    private readonly AppDbContext _db;
    private readonly ISettingsService _settings;
    private readonly IAuditService _audit;
    private readonly IUserService _userService;

    public ItemService(AppDbContext db, ISettingsService settings, IAuditService audit, IUserService userService)
    {
        _db = db;
        _settings = settings;
        _audit = audit;
        _userService = userService;
    }

    public async Task<List<Item>> GetAllAsync(string? search = null)
    {
        var query = _db.Items.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(i =>
                i.ItemCode.ToLower().Contains(search) ||
                i.ItemName.ToLower().Contains(search) ||
                i.Category.ToLower().Contains(search) ||
                i.Supplier.ToLower().Contains(search));
        }

        return await query.OrderBy(i => i.ItemCode).ToListAsync();
    }

    public async Task<Item?> GetByCodeAsync(string itemCode)
    {
        return await _db.Items.FirstOrDefaultAsync(i => i.ItemCode == itemCode);
    }

    public async Task<Item> CreateAsync(Item item)
    {
        if (string.IsNullOrWhiteSpace(item.ItemCode))
            item.ItemCode = await _settings.GetNextItemCodeAsync();

        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = _userService.CurrentUser?.Username ?? "SYSTEM";
        item.Status = "Active";

        _db.Items.Add(item);
        await _db.SaveChangesAsync();

        await _audit.LogAsync("ITEM_CREATE", $"Created item {item.ItemCode} - {item.ItemName}");
        return item;
    }

    public async Task UpdateAsync(Item item)
    {
        item.UpdatedAt = DateTime.UtcNow;
        _db.Items.Update(item);
        await _db.SaveChangesAsync();

        await _audit.LogAsync("ITEM_UPDATE", $"Updated item {item.ItemCode}");
    }

    public async Task DeactivateAsync(string itemCode)
    {
        var item = await GetByCodeAsync(itemCode);
        if (item == null) return;

        item.Status = "Inactive";
        item.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        await _audit.LogAsync("ITEM_DEACTIVATE", $"Deactivated item {itemCode}");
    }
}