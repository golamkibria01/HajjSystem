using Microsoft.EntityFrameworkCore;
using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly HajjSystemContext _context;

    public CompanyRepository(HajjSystemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _context.Companies.AsNoTracking().ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies.FindAsync(id);
    }

    public async Task<Company> AddAsync(Company company)
    {
        var entry = await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

/*
    public async Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
    }
*/
    public async Task<Company> UpdateAsync(Company company)
    {
        _context.Entry(company).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Companies.FindAsync(id);
        if (entity is null) return false;
        _context.Companies.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Companies.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByCrNumberAsync(string crNumber)
    {
        return await _context.Companies.AnyAsync(c => c.CrNumber == crNumber);
    }
}
