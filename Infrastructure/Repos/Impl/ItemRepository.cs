using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public class ItemRepository(ShopContext _db) : IItemRepository
{
    public async Task AddAsync(Item item)
    {
        await _db.Items.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task<Item?> GetItemByIdAsync(long Id)
    {
        Item? item = await _db.Items.FindAsync(Id);
        return item;
    }

    public async Task RemoveAsync(long Id)
    {
        await _db.Items.Where(i => i.Id == Id).ExecuteDeleteAsync();
    }

    public async Task<Item?> RestoreItemAsync(long ItemId)
    {
        Item? item = await _db.Items.Where(i => i.Id == ItemId).FirstOrDefaultAsync();
        if (item == null) return null!;
        item!.IsDeleted = false;
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task SetItemCategoryAsync(long ItemId, long? CategoryId)
    {
        Item? item = await _db.Items.Where(i => i.Id == ItemId).FirstOrDefaultAsync();
        if (item != null)
        {
            item.CategoryId = CategoryId;
            await _db.SaveChangesAsync();
        }
    }

    public async Task SetItemDescriptionAsync(long ItemId, string Description)
    {
        Item? item = _db.Items.Where(i => i.Id == ItemId).FirstOrDefault();
        if (item is not null)
        {
            item.Description = Description;
            await _db.SaveChangesAsync();
        }
    }

    public async Task SetItemDiscountAsync(long ItemId, double Discount)
    {
        Item? item = _db.Items.Where(i => i.Id == ItemId).FirstOrDefault();
        if (item is not null)
        {
            item.Discount = Discount;
            await _db.SaveChangesAsync();
        }
    }

    public async Task SetItemNameAsync(long ItemId, string Name)
    {
        Item? item = _db.Items.Where(i => i.Id == ItemId).FirstOrDefault();
        if (item is not null)
        {
            item.Name = Name;
            await _db.SaveChangesAsync();
        }
    }

    public async Task SetItemPictureAsync(long ItemId, string URI)
    {
        Item? item = _db.Items.Where(i => i.Id == ItemId).FirstOrDefault();
        if (item is not null)
        {
            item.ImageUrl = URI;
            await _db.SaveChangesAsync();
        }
    }

    public async Task SetItemPriceAsync(long ItemId, decimal Price)
    {
        Item? item = _db.Items.Where(i => i.Id == ItemId).FirstOrDefault();
        if (item is not null)
        {
            item.Price = Price;
            await _db.SaveChangesAsync();
        }
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