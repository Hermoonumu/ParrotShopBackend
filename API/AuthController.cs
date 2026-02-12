using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Services;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/auth")]


public class AuthController(IAuthService _authSvc, IUserService _userSvc, IConfiguration _conf) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegFormDTO rfDTO)
    {
        Dictionary<string, string> tokens = await _authSvc.RegisterAsync(rfDTO);
        await ComposeCookies(HttpContext, tokens);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginFormDTO lfDTO)
    {
        Dictionary<string, string> tokens = await _authSvc.LoginAsync(lfDTO);
        await ComposeCookies(HttpContext, tokens);
        return Ok();
    }

    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {

        User? user = await _authSvc.AuthenticateUserAsync(HttpContext.Request.Cookies["AccessToken"]!);

        return Ok(new { Message = $"You are {user!.Name}! Username: {user!.Username}; Access level: {user!.Role}" });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        Dictionary<string, string> tokens = new();
        tokens = await _authSvc
                            .AttemptRefreshAsync(
                                HttpContext.Request.Cookies["RefreshToken"]!
                                );
        await ComposeCookies(HttpContext, tokens);
        return Ok();
    }


    private async Task ComposeCookies(HttpContext ctx, Dictionary<string, string> tokens, bool expired = false)
    {
        var CookieOpt = new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };
        if (expired) CookieOpt.Expires = DateTime.UtcNow.Add(TimeSpan.FromDays(-1));
        if (!expired) CookieOpt.Expires = DateTime.UtcNow.Add(TimeSpan.FromDays(Int32.Parse(_conf["SecSettings:RefreshDurationDays"]!)));
        ctx.Response.Cookies.Append("RefreshToken", expired ? "" : tokens["RefreshToken"], CookieOpt);
        if (!expired) CookieOpt.Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(Int32.Parse(_conf["SecSettings:TokenDurationMinutes"]!)));
        ctx.Response.Cookies.Append("AccessToken", expired ? "" : tokens["AccessToken"], CookieOpt);
    }


    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authSvc.ClearTokensAsync(HttpContext.Request.Cookies["AccessToken"]!,
                                        HttpContext.Request.Cookies["RefreshToken"]!);
        await ComposeCookies(HttpContext, [], true);
        return Ok();
    }



}