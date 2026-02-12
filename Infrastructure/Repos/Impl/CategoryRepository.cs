using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;




public class CategoryRepository(ShopContext _db) : ICategoryRepository
{
    public async Task AddAsync(ItemCategory category)
    {
        await _db.ItemCategories.AddAsync(category);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(long Id)
    {
        await _db.ItemCategories.Where(c => c.Id == Id).ExecuteDeleteAsync();
    }

    public async Task SetCategoryDescriptionAsync(long CategoryId, string Description)
    {
        ItemCategory? cat = await _db.ItemCategories.Where(c => c.Id == CategoryId)
                                                    .FirstOrDefaultAsync();


    }

    public Task SetCategoryNameAsync(long CategoryId, string Name)
    {
        throw new NotImplementedException();
    }
}