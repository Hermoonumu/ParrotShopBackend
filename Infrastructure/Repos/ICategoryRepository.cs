using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;



public interface ICategoryRepository
{
    public Task AddAsync(ItemCategory category);
    public Task RemoveAsync(long Id);
    public Task SetCategoryNameAsync(long CategoryId, string Name);
    public Task SetCategoryDescriptionAsync(long CategoryId, string Description);
}