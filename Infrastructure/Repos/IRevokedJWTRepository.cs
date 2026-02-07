using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public interface IRevokedJWTRepository
{
    public Task AddTokenAsync(RevokedJWT token);
}