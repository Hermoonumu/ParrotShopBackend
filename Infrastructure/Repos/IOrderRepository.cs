using ParrotShopBackend.Domain;

namespace ParrotShopBackend.Infrastructure.Repos;

public interface IOrderRepository
{
    public Task AddOrderItemAsync(OrderItem oItem);
    public Task SaveOrderAsync(Order order);
    public Task SaveChangedOrderAsync();
    public Task GetAllUserOrders(User user);
}