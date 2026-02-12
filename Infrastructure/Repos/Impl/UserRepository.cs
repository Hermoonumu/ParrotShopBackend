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

    public async Task<User?> GetUserByIdAsync(long id)
    {
        return await _db.Users.Include(u => u.Cart).Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _db.Users.Include(u => u.Cart).Where(u => u.Username == username).FirstOrDefaultAsync();
    }

    public async Task RemoveUserByUsernameAsync(string username)
    {
        await _db.Users.Where(u => u.Username == username).ExecuteDeleteAsync();
        await _db.SaveChangesAsync();
    }
    public async Task<User?> CheckIfExists(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

}