using ParrotShopBackend.Application.DTO;

namespace ParrotShopBackend.Application.Services;



public interface ICategoryService
{
    public Task CreateNewCategoryAsync(CategoryDTO cDTO);
    public Task RemoveCategoryAsync(long Id);
    public Task SetCategoryNameAsync(long CategoryId, string Name);
    public Task SetCategoryDescriptionAsync(long CategoryId, string Description);

}
