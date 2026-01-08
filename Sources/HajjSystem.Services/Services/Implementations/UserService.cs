using static BCrypt.Net.BCrypt;
using HajjSystem.Data;
using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;
using HajjSystem.Services.Interfaces;

namespace HajjSystem.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ICompanyService _companyService;
    private readonly IUserRoleService _userRoleService;
    private readonly IRoleService _roleService;
    private readonly HajjSystemContext _context;

    public UserService(IUserRepository repository, ICompanyService companyService, IUserRoleService userRoleService, IRoleService roleService, HajjSystemContext context)
    {
        _repository = repository;
        _companyService = companyService;
        _userRoleService = userRoleService;
        _roleService = roleService;
        _context = context;
    }

    public async Task<OperationResponse> CreateCustomerAsync(CustomerUserCreationModel model)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = new User
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Username = model.Username,
                Password = HashPassword(model.Password),
                Email = model.Email,
                UserType = UserType.Customer,
                SeasonId = model.SeasonId
            };

            var createdUser = await _repository.CreateAsync(user);
            
            // Find and assign Customer role
            var customerRole = await _roleService.GetByNameAsync("Customer");
            if (customerRole == null)
            {
                await transaction.RollbackAsync();
                return new OperationResponse { Status = false, Message = "Something Went wrong. Please contact to support" };
            }

            await _userRoleService.CreateAsync(new UserRole
            {
                UserId = createdUser.Id,
                RoleId = customerRole.Id
            });

            await transaction.CommitAsync();
            return new OperationResponse { Status = true, Message = "User created successfully" };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new OperationResponse { Status = false, Message = $"Failed to register : {ex.Message}" };
        }
    }

    public async Task<OperationResponse> CreateCompanyUserAsync(CompanyUserCreationModel model)
    {
        // Check if company with this CrNumber already exists
        var companyExists = await _companyService.ExistsByCrNumberAsync(model.CrNumber);
        if (companyExists)
        {
            return new OperationResponse { Status = false, Message = "Company already exists. Please contact with the owner or support center" };
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Create company first
            var company = new Company
            {
                CompanyName = model.CompanyName,
                CrNumber = model.CrNumber
            };

            var createdCompany = await _companyService.CreateAsync(company);

            // Create user with company reference
            var user = new User
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Username = model.Username,
                Password = HashPassword(model.Password),
                Email = model.Email,
                UserType = UserType.CompanyUser,
                CompanyId = createdCompany.Id,
                SeasonId = model.SeasonId
            };

            var createdUser = await _repository.CreateAsync(user);

            // Find and assign Owner role
            var ownerRole = await _roleService.GetByNameAsync("Owner");
            if (ownerRole == null)
            {
                await transaction.RollbackAsync();
                return new OperationResponse { Status = false, Message = "Owner role not found. Please contact support" };
            }

            await _userRoleService.CreateAsync(new UserRole
            {
                UserId = createdUser.Id,
                RoleId = ownerRole.Id
            });

            await transaction.CommitAsync();
            return new OperationResponse { Status = true, Message = "Company registered successfully" };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new OperationResponse { Status = false, Message = $"Failed to register company: {ex.Message}" };
        }
    }

    public async Task<LoginResponse?> LoginAsync(LoginModel model)
    {
        var user = await _repository.GetByUsernameAsync(model.Username);
        
        if (user == null)
        {
            return null;
        }

        // Verify password using BCrypt
        bool isPasswordValid = Verify(model.Password, user.Password);
        
        if (!isPasswordValid)
        {
            return null;
        }

        // Get user roles from DB
        var userRoles = await _userRoleService.GetByUserIdAsync(user.Id);
        var roleNames = userRoles.Select(ur => ur.Role?.Name).Where(name => !string.IsNullOrEmpty(name)).ToList();

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            UserType = user.UserType.ToString(),
            SeasonId = user.SeasonId,
            Roles = roleNames!
        };
    }
}
