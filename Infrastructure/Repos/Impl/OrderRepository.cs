using Microsoft.EntityFrameworkCore;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;


public class OrderRepository(ShopContext _db) : IOrderRepository
{
    public async Task AddOrderItemAsync(OrderItem oItem)
    {
        await _db.OrderItems.AddAsync(oItem);
    }

    public Task GetAllUserOrders(User user)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangedOrderAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task SaveOrderAsync(Order order)
    {
        await _db.Orders.AddAsync(order);
    }
}