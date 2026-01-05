using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;

namespace HajjSystem.Services.Services;

public class SeasonService : ISeasonService
{
    private readonly ISeasonRepository _repository;

    public SeasonService(ISeasonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Season>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Season?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Season> CreateAsync(Season season)
    {
        return await _repository.AddAsync(season);
    }

    public async Task<Season> UpdateAsync(Season season)
    {
        return await _repository.UpdateAsync(season);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
