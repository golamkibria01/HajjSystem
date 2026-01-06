using Microsoft.EntityFrameworkCore;
using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly HajjSystemContext _context;

    public RoleRepository(HajjSystemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles.AsNoTracking().ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => EF.Functions.Like(r.Name, $"%{name}%"));
    }

    public async Task<Role> AddAsync(Role role)
    {
        var entry = await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        _context.Entry(role).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Roles.FindAsync(id);
        if (entity is null) return false;
        _context.Roles.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Roles.AnyAsync(r => r.Id == id);
    }
}
