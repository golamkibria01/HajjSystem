using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;
using HajjSystem.Services.Interfaces;

namespace HajjSystem.Services.Implementations;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;

    public UserRoleService(IUserRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<UserRole?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task<UserRole> CreateAsync(UserRole userRole)
    {
        return await _repository.AddAsync(userRole);
    }

    public async Task<UserRole> UpdateAsync(UserRole userRole)
    {
        return await _repository.UpdateAsync(userRole);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
