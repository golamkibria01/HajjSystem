using HajjSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HajjSystem.Data.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<Company> AddAsync(Company company);
    Task<Company> UpdateAsync(Company company);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsByCrNumberAsync(string crNumber);
}
