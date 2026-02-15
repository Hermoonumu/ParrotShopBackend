using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public class ItemRepository(ShopContext _db) : IItemRepository
{
    public async Task AddAsync(Item item)
    {
        await _db.Items.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public Task<List<Item>> GetAllItemsAsync(bool ignoreSoftDelFilter = false)
    {
        if (ignoreSoftDelFilter) return _db.Items.IgnoreQueryFilters().ToListAsync();
        return _db.Items.ToListAsync();
    }

    public async Task<Item?> GetItemByIdAsync(long Id)
    {
        Item? item = await _db.Items.FindAsync(Id);
        return item;
    }

    public async Task RemoveAsync(long Id)
    {
        var item = await _db.Items.IgnoreQueryFilters()
                                    .Where(i => i.Id == Id)
                                    .FirstOrDefaultAsync();
        if (item is null) return;
        _db.Items.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<Item?> RestoreItemAsync(long ItemId)
    {
        Item? item = await _db.Items.FindAsync(ItemId);
        if (item == null) return null!;
        item!.IsDeleted = false;
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task SoftDeleteItemAsync(long ItemId)
    {
        Item? item = _db.Items.Where(i => i.Id == ItemId).FirstOrDefault();
        if (item is not null)
        {
            item.IsDeleted = true;
            await _db.SaveChangesAsync();
        }
    }

    public async Task UpdateItemAsync()
    {
        await _db.SaveChangesAsync();
    }
}