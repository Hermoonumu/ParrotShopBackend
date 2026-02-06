using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public interface IUserRepository
{
    public Task AddUserToDBAsync(User user);
    public Task<User> GetUserByUsernameAsync(string username);

}