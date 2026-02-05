using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using ParrotShopBackend.Application.DTO;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/auth")]


public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegFormDTO rfDTO)
    {
        return Forbid();
    }

    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> TestAuth()
    {
        return Ok();
    }



}