using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;
using HajjSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HajjSystem.Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _service;

    public RegistrationController(IRegistrationService service)
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegistrationCreateModel model)
    {
        var entity = new Registration
        {
            Name = model.Name,
            NationalId = model.NationalId
        };

        var created = await _service.CreateAsync(entity);
        return Ok(created);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Registration registration)
    {
        var exists = await _service.GetByIdAsync(registration.Id);
        if (exists is null) return NotFound();

        await _service.UpdateAsync(registration);
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
