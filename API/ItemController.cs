using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Services;
using ParrotShopBackend.Domain;

namespace ParrotShopBackend.API;

[ApiController]
[Route("/api/items")]
public class ItemController(IItemService _itemSvc) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CreateNewItem([FromBody] NewItemDTO iDTO)
    {
        await _itemSvc.CreateNewItemAsync(iDTO);
        return Ok();
    }
    [HttpPatch("{Id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> UpdateItem([FromRoute] long Id, [FromBody] JsonPatchDocument<Item> patchDoc)
    {
        bool res = await _itemSvc.UpdateItemAsync(Id, patchDoc);
        if (res) return Ok();
        else return BadRequest();
    }
}