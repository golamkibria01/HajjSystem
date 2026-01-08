using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;
using HajjSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HajjSystem.Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;

    public CompanyController(ICompanyService service)
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
    public async Task<IActionResult> Create([FromBody] CompanyCreateModel model)
    {
        var company = new Company
        {
            CompanyName = model.CompanyName,
            CrNumber = model.CrNumber,
            Address = model.Address,
            Mobile = model.Mobile,
            VatRegNumber = model.VatRegNumber
        };
        
        var created = await _service.CreateAsync(company);
        
        return Ok(new OperationResponse 
        { 
            Status = true, 
            Message = "Company created successfully" 
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CompanyUpdateModel model)
    {
        var company = new Company
        {
            Id = model.Id,
            CompanyName = model.CompanyName,
            CrNumber = model.CrNumber,
            Address = model.Address,
            Mobile = model.Mobile,
            VatRegNumber = model.VatRegNumber
        };
        
        var updated = await _service.UpdateAsync(company);
        
        return Ok(new OperationResponse 
        { 
            Status = true, 
            Message = "Company updated successfully" 
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
                Message = "Company not found" 
            });
        }
        
        return Ok(new OperationResponse 
        { 
            Status = true, 
            Message = "Company deleted successfully" 
        });
    }
}
