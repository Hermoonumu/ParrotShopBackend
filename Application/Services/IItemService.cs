using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Application.Services;


public interface IItemService
{
    public Task CreateNewItemAsync(ItemDTO iDTO);
    public Task RemoveItemAsync(long Id);
    public Task UpdateItemAsync(ItemDTO iDTO);
    public Task SoftDeleteItemAsync(long ItemId);
    public Task<Item?> RestoreItemAsync(long ItemId);

}


