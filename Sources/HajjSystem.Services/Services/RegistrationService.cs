using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;

namespace HajjSystem.Services.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _repository;

    public RegistrationService(IRegistrationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Registration>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Registration?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Registration> CreateAsync(Registration registration)
    {
        return await _repository.AddAsync(registration);
    }

    public async Task UpdateAsync(Registration registration)
    {
        await _repository.UpdateAsync(registration);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
