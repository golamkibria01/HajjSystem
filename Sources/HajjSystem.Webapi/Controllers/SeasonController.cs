using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;
using HajjSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HajjSystem.Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeasonController : ControllerBase
{
    private readonly ISeasonService _service;

    public SeasonController(ISeasonService service)
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
    public async Task<IActionResult> Create([FromBody] SeasonCreateModel model)
    {
        var season = new Season
        {
            Title = model.Title,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            isCurrent = model.isCurrent
        };
        
        var created = await _service.CreateAsync(season);
        
        return Ok(new OperationResponse 
        { 
            Status = true, 
            Message = "Season created successfully" 
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SeasonUpdateModel model)
    {
        var season = new Season
        {
            Id = model.Id,
            Title = model.Title,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            isCurrent = model.isCurrent
        };
        
        var updated = await _service.UpdateAsync(season);
        
        return Ok(new OperationResponse 
        { 
            Status = true, 
            Message = "Season updated successfully" 
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        
        if (!deleted)
        {
            return Ok(new OperationResponse 
            { 
                Status = false, 
                Message = "Season not found" 
            });
        }
        
        return Ok(new OperationResponse 
        { 
            Status = true, 
            Message = "Season deleted successfully" 
        });
    }
}
