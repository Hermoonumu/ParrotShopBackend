using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotShopBackend.Application.Services;

namespace ParrotShopBackend.API;

[ApiController]
[Route("api/categories")]
public class CategoryController(ICategoryService _catSvc) : ControllerBase
{
    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCategory()
    {
        return Ok();
    }
}
