using Microsoft.EntityFrameworkCore;
using Npgsql;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;



public class RevokedJWTRepository(ShopContext _db) : IRevokedJWTRepository
{
    public async Task AddTokenAsync(RevokedJWT token)
    {
        await _db.revokedJWTs.AddAsync(token);
        await _db.SaveChangesAsync();
    }
}