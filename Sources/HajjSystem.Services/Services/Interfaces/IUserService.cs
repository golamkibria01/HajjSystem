using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;

namespace HajjSystem.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateCustomerAsync(CustomerUserCreationModel model);
    Task<string> CreateCompanyUserAsync(CompanyUserCreationModel model);
    Task<LoginResponse?> LoginAsync(LoginModel model);
}
