using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public interface IUserRepository
{
    public Task AddUserToDBAsync(User user);
    Task<User?> GetUserByIdAsync(long id, bool includeCart = false);
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task RemoveUserByUsernameAsync(string username);
    public Task<User?> CheckIfExists(string username);
    public Task ClearCart(User user);
}

