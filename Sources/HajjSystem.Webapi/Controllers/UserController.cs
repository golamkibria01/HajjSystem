using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;
using HajjSystem.Services.Services;
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

    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerUserCreationModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.CreateCustomerAsync(model);
        return Ok(new { message = result });
    }

    [HttpPost("company")]
    public async Task<IActionResult> CreateCompanyUser([FromBody] CompanyUserCreationModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.CreateCompanyUserAsync(model);
        return Ok(new { message = result });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _service.LoginAsync(model);
        
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        // Remove password from response
        user.Password = string.Empty;
        
        return Ok(new 
        { 
            message = "Login successful",
            user = user
        });
    }
}
