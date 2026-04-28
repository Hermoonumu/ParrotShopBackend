using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Application.Mappers;
using ParrotShopBackend.Application.Services;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/orders")]

public class OrderController(IUserService _userSvc) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        User user = await _userSvc.GetUserByToken(User, true);
        return Ok(user.Orders.Select(o => OrderMapper.FromOrder(o)).ToList());
    }
}