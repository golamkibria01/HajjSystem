using Microsoft.EntityFrameworkCore;
using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HajjSystemContext _context;

    public UserRepository(HajjSystemContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        var entry = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }
}
