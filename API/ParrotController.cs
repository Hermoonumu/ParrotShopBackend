using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Exceptions;
using ParrotShopBackend.Application.Services;
using ParrotShopBackend.Domain;
namespace ParrotShopBackend.API;




[ApiController]
[Route("/api/parrots")]

public class ParrotController(IParrotService _parrotSvc) : ControllerBase
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

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetParrot([FromRoute] long Id, [FromQuery] bool includeTraits = false)
    {
        Parrot? parrot = await _parrotSvc.GetParrotByIdAsync(Id, includeTraits);
        if (parrot is null) return NotFound();
        return Ok(parrot);
    }


    [Authorize(Policy = "Admin")]
    [HttpPost("")]
    public async Task<IActionResult> AddParrot([FromBody] NewParrotDTO npDTO)
    {
        await _parrotSvc.CreateNewParrotAsync(npDTO);
        return Ok();
    }
    [HttpPatch("{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> UpdateItem([FromRoute] long Id, [FromBody] JsonPatchDocument<Parrot> patchDoc)
    {
        bool hasError = await _parrotSvc.UpdateParrotAsync(Id, patchDoc);
        if (hasError) return BadRequest();
        else return Ok();
    }
    [HttpDelete("{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteParrot([FromRoute] long Id, [FromBody] DelConfDTO dcDTO)
    {
        if (dcDTO.Confirmation != "I know it's irreversible") 
            return BadRequest(new {Message = "If you acknowledge that you wish to do that "+
                            "pass a string into body saying \"I know it's irreversible\". Otherwise consider soft delete."});
        await _parrotSvc.RemoveParrotAsync(Id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParrots([FromQuery] bool ignoreSoftDelFilter = false)
    {
        return Ok(await _parrotSvc.GetAllParrotsAsync(ignoreSoftDelFilter));
    }

    [HttpPost("BatchAdd")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> BatchAddParrots([FromBody] List<NewParrotDTO> npDTOs)
    {
        foreach (NewParrotDTO npDTO in npDTOs)
        {
            await _parrotSvc.CreateNewParrotAsync(npDTO);
        }
        return Ok();
    }
    [HttpPost("Traits/{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> AddTraitToParrot([FromRoute] long Id, [FromBody] TraitsDTO tDTO)
    {
        try{
            await _parrotSvc.AddTraitToParrotAsync(Id, tDTO);
        } catch (ParrotDoesntExistException)
        {
            return NotFound();
        } 
        return Ok();
    
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterParrots([FromQuery] ParrotFilterDTO pfDTO)
    {
        return Ok(await _parrotSvc.FilterParrotsAsync(pfDTO));
    }
        
}