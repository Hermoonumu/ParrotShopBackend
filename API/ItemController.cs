using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Application.Services;

namespace ParrotShopBackend.API;

[ApiController]
[Route("/api/items")]
public class ItemController(IItemService _itemSvc) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CreateNewItem([FromBody] ItemDTO iDTO)
    {
        await _itemSvc.CreateNewItemAsync(iDTO);
        return Ok();
    }
    [HttpPatch]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> UpdateItem([FromBody] ItemDTO iDTO)
    {
        await _itemSvc.UpdateItemAsync(iDTO);
        return Ok();
    }
}