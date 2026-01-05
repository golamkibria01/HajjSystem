using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;

namespace HajjSystem.Services.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Company> CreateAsync(Company company)
    {
        return await _repository.AddAsync(company);
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        return await _repository.UpdateAsync(company);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
