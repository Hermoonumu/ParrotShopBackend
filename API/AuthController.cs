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


public class AuthController(IAuthService _authSvc, IConfiguration _conf) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegFormDTO rfDTO)
    {
        try
        {
            Dictionary<string, string> tokens = await _authSvc.RegisterAsync(rfDTO);

            HttpContext.Response.Cookies.Append("RefreshToken", tokens["RefreshToken"],
            new CookieOptions()
            {
                Expires = DateTime.Now.Add(TimeSpan.FromDays(Int32.Parse(_conf["SecSettings:RefreshDurationDays"]!))),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });
            HttpContext.Response.Cookies.Append("AccessToken", tokens["AccessToken"],
            new CookieOptions()
            {
                Expires = DateTime.Now.Add(TimeSpan.FromDays(Int32.Parse(_conf["SecSettings:RefreshDurationDays"]!))),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            }
                );
            return Ok();
        }
        catch (UserAlreadyExistsException e)
        {
            return Conflict(new { e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = $"The server went kaboom. Reason:\n{e.Message}" });
        }
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromBody] LoginFormDTO lfDTO)
    {
        Dictionary<string, string> tokens = await _authSvc.LoginAsync(lfDTO);
        HttpContext.Response.Cookies.Append("RefreshToken", tokens["RefreshToken"],
new CookieOptions()
{
    Expires = DateTime.Now.Add(TimeSpan.FromDays(Int32.Parse(_conf["SecSettings:RefreshDurationDays"]!))),
    HttpOnly = true,
    Secure = true,
    IsEssential = true,
    SameSite = SameSiteMode.None
});
        HttpContext.Response.Cookies.Append("AccessToken", tokens["AccessToken"],
        new CookieOptions()
        {
            Expires = DateTime.Now.Add(TimeSpan.FromDays(Int32.Parse(_conf["SecSettings:RefreshDurationDays"]!))),
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        }
            );
        return Ok();
    }

    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok();
    }




}