using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Exceptions;
using ParrotShopBackend.Application.Services;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/auth")]


public class AuthController(IAuthService _authSvc) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegFormDTO rfDTO)
    {
        try
        {
            await _authSvc.RegisterAsync(rfDTO);
            return Ok();
        }
        catch (UserAlreadyExistsException e)
        {
            return Conflict(new { e.Message });
        }
        catch
        {
            return StatusCode(500, new { Message = "The server went kaboom." });
        }
    }

    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> TestAuth()
    {
        return Ok();
    }



}