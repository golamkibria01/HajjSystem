using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public interface ISeasonRepository
{
    Task<IEnumerable<Season>> GetAllAsync();
    Task<Season?> GetByIdAsync(int id);
    Task<Season> AddAsync(Season season);
    Task<Season> UpdateAsync(Season season);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
