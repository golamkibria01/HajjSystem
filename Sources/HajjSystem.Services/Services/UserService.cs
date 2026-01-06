using static BCrypt.Net.BCrypt;
using HajjSystem.Data;
using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;

namespace HajjSystem.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly HajjSystemContext _context;

    public UserService(IUserRepository repository, ICompanyRepository companyRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, HajjSystemContext context)
    {
        _repository = repository;
        _companyRepository = companyRepository;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
        _context = context;
    }

    public async Task<string> CreateCustomerAsync(CustomerUserCreationModel model)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var entity = new User
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

            var createdUser = await _repository.CreateAsync(entity);
            
            // Find and assign Customer role
            var customerRole = await _roleRepository.GetByNameAsync("Customer");
            if (customerRole == null)
            {
                await transaction.RollbackAsync();
                return "Customer role not found. Please contact support";
            }

            await _userRoleRepository.AddAsync(new UserRole
            {
                UserId = createdUser.Id,
                RoleId = customerRole.Id
            });

            await transaction.CommitAsync();
            return "User created successfully";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return $"Failed to register : {ex.Message}";
        }
    }

    public async Task<string> CreateCompanyUserAsync(CompanyUserCreationModel model)
    {
        // Check if company with this CrNumber already exists
        var companyExists = await _companyRepository.ExistsByCrNumberAsync(model.CrNumber);
        if (companyExists)
        {
            return "Company already exists. Please contact with the owner or support center";
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

            var createdCompany = await _companyRepository.AddAsync(company);

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
            var ownerRole = await _roleRepository.GetByNameAsync("Owner");
            if (ownerRole == null)
            {
                await transaction.RollbackAsync();
                return "Owner role not found. Please contact support";
            }

            await _userRoleRepository.AddAsync(new UserRole
            {
                UserId = createdUser.Id,
                RoleId = ownerRole.Id
            });

            await transaction.CommitAsync();
            return "Company registered successfully";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return $"Failed to register company: {ex.Message}";
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
        var userRoles = await _userRoleRepository.GetByUserIdAsync(user.Id);
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
