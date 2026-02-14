using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParrotShopBackend.Domain;
namespace ParrotShopBackend.API;




[ApiController]
[Route("/api/parrots")]

public class ParrotController : ControllerBase
{
    [HttpPost("testColourHandling")]
    public async Task<IActionResult> TestColourHandling([FromBody] List<Color> colours)
    {
        Color AllColours = default;
        foreach (Color col in colours) AllColours |= col;
        Parrot parrot = new Parrot { Name = "Chum", Price = 100 };
        parrot.ColorType = AllColours;

        return Ok(new {Msg="WOW IT WORKED!", Parrot = parrot, AllColours});
    }


    [Authorize(Policy = "Admin")]
    [HttpPost("")]
    public async Task<IActionResult> AddParrot()
    {
        return Ok();
    }
}