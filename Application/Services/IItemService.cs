using Microsoft.AspNetCore.JsonPatch;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Application.Services;


public interface IItemService
{
    public Task CreateNewItemAsync(NewItemDTO iDTO);
    public Task RemoveItemAsync(long Id);
    public Task<bool> UpdateItemAsync(long Id, JsonPatchDocument<Item> patchDoc);
    public Task SoftDeleteItemAsync(long ItemId);
    public Task<Item?> RestoreItemAsync(long ItemId);

}


