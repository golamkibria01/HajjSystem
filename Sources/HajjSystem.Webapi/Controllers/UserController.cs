using HajjSystem.Models.Models;
using HajjSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HajjSystem.Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerUserCreationModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.CreateCustomerAsync(model);
        return Ok(new { message = result });
    }

    [AllowAnonymous]
    [HttpPost("company")]
    public async Task<IActionResult> CreateCompanyUser([FromBody] CompanyUserCreationModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.CreateCompanyUserAsync(model);
        return Ok(new { message = result });
    }
}
