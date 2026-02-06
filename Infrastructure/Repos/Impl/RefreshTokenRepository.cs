using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public class RefreshTokenRepository(ShopContext _db) : IRefreshTokenRepository
{
    public async Task AddTokenAsync(RefreshToken token)
    {
        await _db.RefreshTokens.AddAsync(token);
        await _db.SaveChangesAsync();
    }

    public async Task<List<RefreshToken>> GetAllTokensAsync(User user)
    {
        return await _db.RefreshTokens.Where(tk => tk.UserID == user.Id).ToListAsync();
    }

    public async Task RemoveAllUserTokensAsync(long UserId)
    {
        await _db.RefreshTokens.Where(tk => tk.UserID == UserId).ExecuteDeleteAsync();
        await _db.SaveChangesAsync();
    }
}