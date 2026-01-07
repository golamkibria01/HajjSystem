using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;
using HajjSystem.Services.Interfaces;

namespace HajjSystem.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Role> CreateAsync(Role role)
    {
        return await _repository.AddAsync(role);
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        return await _repository.UpdateAsync(role);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
