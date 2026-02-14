using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Domain;
using System.Text;
using System.Text.Json;

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


    [HttpGet("DumpMyParrot")]
    public async Task<IActionResult> DumpMyParrot()
    {
        Parrot parrot = new Parrot { Name = "Chum", Price = 100 };
        string json = JsonSerializer.Serialize(parrot);
        Object deserialized = JsonSerializer.Deserialize<Object>(json)!;

        return Ok(new {Message="Here's what we have: ", json, deserialized, Type=deserialized is Parrot});
    }


    [HttpGet("oddEvenBitOp")]
    public async Task<IActionResult> OddEvenBitOp([FromQuery] int number)
    {
        return Ok(new { res = (number & 1) == 0 ? "Even" : "Odd", debug = number & 1 });
    }

    [HttpGet("AmIATeapot")]
    public async Task<IActionResult> AmIATeapot()
    {
        return StatusCode(418, new { Message = "Yes you are a teapot" });

    }
}
