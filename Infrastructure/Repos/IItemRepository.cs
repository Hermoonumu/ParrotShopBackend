
using Microsoft.AspNetCore.JsonPatch;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public interface IItemRepository
{
    public Task AddAsync(Item item);
    public Task<Item?> GetItemByIdAsync(long Id);
    public Task RemoveAsync(long Id);
    public Task<Item?> RestoreItemAsync(long ItemId);
    public Task SetItemCategoryAsync(long ItemId, long? CategoryId);
    public Task SetItemNameAsync(long ItemId, string Name);
    public Task SetItemDescriptionAsync(long ItemId, string Description);
    public Task SetItemPriceAsync(long ItemId, decimal Price);
    public Task SetItemDiscountAsync(long ItemId, double Discount);
    public Task SetItemPictureAsync(long ItemId, string URI);
    public Task UpdateItemAsync();
    public Task SoftDeleteItemAsync(long ItemId);

}

