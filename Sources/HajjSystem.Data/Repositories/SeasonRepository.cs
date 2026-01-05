using Microsoft.EntityFrameworkCore;
using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public class SeasonRepository : ISeasonRepository
{
    private readonly HajjSystemContext _context;

    public SeasonRepository(HajjSystemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Season>> GetAllAsync()
    {
        return await _context.Seasons.AsNoTracking().ToListAsync();
    }

    public async Task<Season?> GetByIdAsync(int id)
    {
        return await _context.Seasons.FindAsync(id);
    }

    public async Task<Season> AddAsync(Season season)
    {
        var entry = await _context.Seasons.AddAsync(season);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Season> UpdateAsync(Season season)
    {
        _context.Entry(season).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return season;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Seasons.FindAsync(id);
        if (entity is null) return false;
        _context.Seasons.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Seasons.AnyAsync(s => s.Id == id);
    }
}
