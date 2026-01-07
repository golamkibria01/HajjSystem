using HajjSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HajjSystem.Services.Interfaces;

public interface IRegistrationService
{
    Task<IEnumerable<Registration>> GetAllAsync();
    Task<Registration?> GetByIdAsync(int id);
    Task<Registration> CreateAsync(Registration registration);
    Task UpdateAsync(Registration registration);
    Task DeleteAsync(int id);
}
