using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;

public interface IParrotRepository
{
    public Task AddAsync(Parrot parrot);
    public Task<Parrot?> GetParrotByIdAsync(long Id, bool IncludeTraits = false);
    public Task RemoveAsync(long Id);
    public Task<Parrot?> RestoreParrotAsync(long ParrotId);
    public Task SoftDeleteParrotAsync(long ParrotId);
    public Task UpdateParrotAsync();
    public Task<List<Parrot>> GetAllParrotsAsync(bool ignoreSoftDelFilter = false); 

}