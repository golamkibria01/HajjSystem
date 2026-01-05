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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var item = await _service.GetByUsernameAsync(username);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var item = await _service.GetByEmailAsync(email);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var entity = new User
        {
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            Username = model.Username,
            Password = model.Password,
            CompanyId = model.CompanyId,
            Role = model.Role,
            UserType = model.UserType,
            Address = model.Address,
            City = model.City,
            Country = model.Country,
            Passport = model.Passport,
            PassportValidity = model.PassportValidity,
            Mobile = model.Mobile,
            Email = model.Email,
            SeasonId = model.SeasonId
        };

        var created = await _service.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (user.Id != id) return BadRequest("Id mismatch.");

        var exists = await _service.GetByIdAsync(id);
        if (exists is null) return NotFound();

        await _service.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var exists = await _service.GetByIdAsync(id);
        if (exists is null) return NotFound();

        await _service.DeleteAsync(id);
        return NoContent();
    }
}
