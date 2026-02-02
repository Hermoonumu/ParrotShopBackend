using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/TEST")]

public class TestController : ControllerBase
{
    [HttpGet("sendAMail")]
    public async Task<IActionResult> SendAMailAsync()
    {
        return Ok(new { Message = "cummyass" });
    }
}
