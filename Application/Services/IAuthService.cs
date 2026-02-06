using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Application.DTO;

namespace ParrotShopBackend.Application.Services;


public interface IAuthService
{
    public Task RegisterAsync(RegFormDTO rfDTO);
}