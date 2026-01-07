using HajjSystem.Models.Entities;

namespace HajjSystem.Services.Interfaces;

public interface ISeasonService
{
    Task<IEnumerable<Season>> GetAllAsync();
    Task<Season?> GetByIdAsync(int id);
    Task<Season> CreateAsync(Season season);
    Task<Season> UpdateAsync(Season season);
    Task<bool> DeleteAsync(int id);
}
