using Microsoft.AspNetCore.JsonPatch;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Exceptions;
using ParrotShopBackend.Application.Extensions;
using ParrotShopBackend.Application.Mappers;
using ParrotShopBackend.Domain;
using ParrotShopBackend.Infrastructure.Repos;

namespace ParrotShopBackend.Application.Services;



public class OrderService(IOrderRepository _ordRepo) : IOrderService
{
}