using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public class ParrotRepository(ShopContext _db) : IParrotRepository
{
    public async Task AddAsync(Parrot parrot)
    {
        await _db.Parrots.AddAsync(parrot);
        await _db.SaveChangesAsync();
    }

    public async Task<Parrot?> GetParrotByIdAsync(long Id, bool IncludeTraits = false)
    {
        Parrot? parrot;
        if (IncludeTraits)
        {
            parrot = await _db.Parrots.Include(p => p.Traits)
                                        .Where(p => p.Id == Id)
                                        .FirstOrDefaultAsync();
        } else
        {
            parrot = await _db.Parrots.FindAsync(Id);
        }
        if (parrot is null) return null;
        return parrot;
    }

    public async Task RemoveAsync(long Id)
    {
        var parrot = await _db.Parrots.IgnoreQueryFilters()
                                    .Where(i => i.Id == Id)
                                    .FirstOrDefaultAsync();
        if (parrot is null) return;
        _db.Parrots.Remove(parrot);
        await _db.SaveChangesAsync();
    }

    public async Task<Parrot?> RestoreParrotAsync(long ParrotId)
    {
        var parrot = await _db.Parrots.FindAsync(ParrotId);
        if (parrot is null) return null;
        parrot.IsDeleted = false;
        await _db.SaveChangesAsync();
        return parrot;
    }

    public async Task SoftDeleteParrotAsync(long ParrotId)
    {
        var parrot = await _db.Parrots.FindAsync(ParrotId);
        if (parrot is null) return;
        parrot.IsDeleted = true;
        await _db.SaveChangesAsync();
    }

    public async Task UpdateParrotAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task<List<Parrot>> GetAllParrotsAsync(bool ignoreSoftDelFilter=false)
    {
        if (ignoreSoftDelFilter) return await _db.Parrots.IgnoreQueryFilters().ToListAsync();
        return await _db.Parrots.ToListAsync();
    }
}