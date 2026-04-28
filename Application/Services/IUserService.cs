using System.Security.Claims;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Application.Services;


public interface IUserService
{
    public Task<User?> GetUserByIdAsync(long Id, bool includeCart = false);
    public Task<User?> GetUserByToken(ClaimsPrincipal principal, bool includeCart = false);

    public Task ClearCart(User user);
}
