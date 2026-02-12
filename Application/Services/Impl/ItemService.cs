using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Mappers;
using ParrotShopBackend.Domain;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.Application.Services;


public class ItemService(IItemRepository _itemRepo) : IItemService
{
    public async Task CreateNewItemAsync(ItemDTO iDTO)
    {
        Item item = ItemMapper.FromItemDTO(iDTO);
        await _itemRepo.AddAsync(item);
    }


    public async Task RemoveItemAsync(long Id)
    {
        await _itemRepo.RemoveAsync(Id);
    }

    public async Task<Item?> RestoreItemAsync(long ItemId)
    {
        return await _itemRepo.RestoreItemAsync(ItemId);
    }


    public async Task SoftDeleteItemAsync(long ItemId)
    {
        await _itemRepo.SoftDeleteItemAsync(ItemId);
    }

    public async Task UpdateItemAsync(ItemDTO iDTO)
    {
        await _itemRepo.UpdateItemAsync(iDTO);
    }
}