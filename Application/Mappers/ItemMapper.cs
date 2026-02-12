using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Application.Mappers;



public class ItemMapper
{
    public static Item FromItemDTO(ItemDTO iDTO)
    {
        return new Item()
        {
            Name = iDTO.Name,
            Description = iDTO.Description,
            Price = iDTO.Price??0,
            ImageUrl = iDTO.ImageUrl,
            CategoryId = iDTO.CategoryId,
            IsDeleted = false
        };
    }
}