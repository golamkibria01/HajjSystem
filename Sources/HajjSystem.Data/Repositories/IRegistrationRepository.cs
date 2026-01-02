using HajjSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HajjSystem.Data.Repositories;

public interface IRegistrationRepository
{
    Task<IEnumerable<Registration>> GetAllAsync();
    Task<Registration?> GetByIdAsync(int id);
    Task<Registration> AddAsync(Registration registration);
    Task UpdateAsync(Registration registration);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
