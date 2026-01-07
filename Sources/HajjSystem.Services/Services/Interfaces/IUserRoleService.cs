using HajjSystem.Models.Entities;

namespace HajjSystem.Services.Interfaces;

public interface IUserRoleService
{
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task<UserRole?> GetByIdAsync(int id);
    Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
    Task<UserRole> CreateAsync(UserRole userRole);
    Task<UserRole> UpdateAsync(UserRole userRole);
    Task<bool> DeleteAsync(int id);
}
