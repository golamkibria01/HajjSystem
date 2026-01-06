using HajjSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HajjSystem.Data.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly HajjSystemContext _context;

    public UserRoleRepository(HajjSystemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await _context.UserRoles
            .Include(ur => ur.User)
            .Include(ur => ur.Role)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserRole?> GetByIdAsync(int id)
    {
        return await _context.UserRoles
            .Include(ur => ur.User)
            .Include(ur => ur.Role)
            .FirstOrDefaultAsync(ur => ur.Id == id);
    }

    public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
    {
        return await _context.UserRoles
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserRole> AddAsync(UserRole userRole)
    {
        var entry = await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<UserRole> UpdateAsync(UserRole userRole)
    {
        _context.Entry(userRole).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return userRole;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.UserRoles.FindAsync(id);
        if (entity is null) return false;
        _context.UserRoles.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.UserRoles.AnyAsync(ur => ur.Id == id);
    }
}
