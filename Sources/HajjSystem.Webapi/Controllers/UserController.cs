using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;
using HajjSystem.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

    [Authorize(Roles = "Admin,Customer")]
    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerUserCreationModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.CreateCustomerAsync(model);
        return Ok(new { message = result });
    }

    [Authorize(Roles = "Admin,Company")]
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

        var loginResponse = await _service.LoginAsync(model);
        
        if (loginResponse == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginResponse.Username),
            new Claim(ClaimTypes.NameIdentifier, loginResponse.UserId.ToString()),
            new Claim("SeasonId", loginResponse.SeasonId.ToString())
        };

        foreach (var roleName in loginResponse.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY"));

        var token = new JwtSecurityToken(
            issuer: "myapi",
            audience: "myclient",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials:
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new 
        { 
            message = "Login successful",
            userId = loginResponse.UserId,
            username = loginResponse.Username,
            email = loginResponse.Email,
            userType = loginResponse.UserType,
            roles = loginResponse.Roles,
            token = jwtToken
        });
    }
}
