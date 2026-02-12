using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.Application.Services;




public class CategoryService(ICategoryRepository _catRepo) : ICategoryService
{
    public Task CreateNewCategoryAsync(CategoryDTO cDTO)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCategoryAsync(long Id)
    {
        throw new NotImplementedException();
    }

    public Task SetCategoryDescriptionAsync(long CategoryId, string Description)
    {
        throw new NotImplementedException();
    }

    public Task SetCategoryNameAsync(long CategoryId, string Name)
    {
        throw new NotImplementedException();
    }
}