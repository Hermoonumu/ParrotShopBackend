using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public interface IRefreshTokenRepository
{
    public Task AddTokenAsync(RefreshToken token);
    public Task RemoveAllUserTokensAsync(long UserId);
    public Task<List<RefreshToken>> GetAllTokensAsync(User user);

}