using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.API;


[ApiController]
[Route("/api/TEST")]

public class TestController : ControllerBase
{
    [HttpGet("parrotTest")]
    public async Task<IActionResult> CheckTheParrot()
    {
        Parrot parrot = new Parrot { Name = "Chum", Price = 100 };
        parrot.ColorType = Color.Black | Color.White | Color.Green | Color.Colourful;

        return Ok(new { Message = $"The parrot has been successfully checked. {parrot.ColorType}" });
    }


    [HttpGet("oddEvenBitOp")]
    public async Task<IActionResult> OddEvenBitOp([FromQuery] int number)
    {
        return Ok(new { res = (number & 1) == 0 ? "Even" : "Odd", debug = number & 1 });
    }


}
