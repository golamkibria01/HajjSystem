using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public interface IUserRoleRepository
{
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task<UserRole?> GetByIdAsync(int id);
    Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
    Task<UserRole> AddAsync(UserRole userRole);
    Task<UserRole> UpdateAsync(UserRole userRole);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
