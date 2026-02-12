using ParrotShopBackend.Domain;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.Application.Services;


public class UserService(IUserRepository _userRepo) : IUserService
{

    public Task<User?> GetUserByIdAsync(long Id)
    {
        return _userRepo.GetUserByIdAsync(Id);

    }
}