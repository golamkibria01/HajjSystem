using HajjSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HajjSystem.Services.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<Company> CreateAsync(Company company);
    Task<Company> UpdateAsync(Company company);
    Task<bool> DeleteAsync(int id);
}
