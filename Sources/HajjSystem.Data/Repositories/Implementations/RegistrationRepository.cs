using Microsoft.EntityFrameworkCore;
using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly HajjSystemContext _context;

    public RegistrationRepository(HajjSystemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Registration>> GetAllAsync()
    {
        return await _context.Registrations.AsNoTracking().ToListAsync();
    }

    public async Task<Registration?> GetByIdAsync(int id)
    {
        return await _context.Registrations.FindAsync(id);
    }

    public async Task<Registration> AddAsync(Registration registration)
    {
        var entry = await _context.Registrations.AddAsync(registration);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(Registration registration)
    {
        _context.Registrations.Update(registration);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Registrations.FindAsync(id);
        if (entity is null) return;
        _context.Registrations.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Registrations.AnyAsync(r => r.Id == id);
    }
}
