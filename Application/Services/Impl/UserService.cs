using System.Security.Claims;
using ParrotShopBackend.Domain;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.Application.Services;


public class UserService(IUserRepository _userRepo) : IUserService
{
    public async Task ClearCart(User user)
    {
        await _userRepo.ClearCart(user);
    }

    public async Task<User?> GetUserByIdAsync(long Id, bool includeCart = false)
    {
        return await _userRepo.GetUserByIdAsync(Id, includeCart);

    }

    public async Task<User?> GetUserByToken(ClaimsPrincipal principal, bool includeCart = false)
    {
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim)) return null;
        return await _userRepo.GetUserByIdAsync(long.Parse(userIdClaim), includeCart);
    }

}