using Microsoft.EntityFrameworkCore;
using Npgsql;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;



public class UserRepository(ShopContext _db) : IUserRepository
{
    public async Task AddUserToDBAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _db.Users.Include(u => u.Cart).Where(u => u.Username == username).FirstAsync();
    }
}