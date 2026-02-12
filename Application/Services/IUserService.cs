using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Application.Services;


public interface IUserService
{
    public Task<User?> GetUserByIdAsync(long Id);
}