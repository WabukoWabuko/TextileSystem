using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Services;

public interface IItemService
{
    Task<List<Item>> GetAllAsync(string? search = null);
    Task<Item?> GetByCodeAsync(string itemCode);
    Task<Item> CreateAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeactivateAsync(string itemCode);
}