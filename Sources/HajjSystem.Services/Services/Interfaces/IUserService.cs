using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;

namespace HajjSystem.Services.Interfaces;

public interface IUserService
{
    Task<OperationResponse> CreateCustomerAsync(CustomerUserCreationModel model);
    Task<OperationResponse> CreateCompanyUserAsync(CompanyUserCreationModel model);
    Task<LoginResponse?> LoginAsync(LoginModel model);
}
