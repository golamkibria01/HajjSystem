using HajjSystem.Models.Models;
using HajjSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HajjSystem.Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _service;

    public AuthController(IUserService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var loginResponse = await _service.LoginAsync(model);
        
        if (loginResponse == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginResponse.Username),
            new Claim(ClaimTypes.NameIdentifier, loginResponse.UserId.ToString()),
            new Claim("SeasonId", loginResponse.SeasonId.ToString()),
            new Claim("UserType", loginResponse.UserType)
        };

        foreach (var roleName in loginResponse.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));
        }

        var jwtSettings = _configuration.GetSection("JwtSettings");

        var key = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(jwtSettings["Key"]));

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(
                Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials:
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new 
        { 
            message = "Login successful",
            token = jwtToken
        });
    }
}
