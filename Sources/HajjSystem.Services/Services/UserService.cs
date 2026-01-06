using static BCrypt.Net.BCrypt;
using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;
using HajjSystem.Models.Models;

namespace HajjSystem.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ICompanyRepository _companyRepository;

    public UserService(IUserRepository repository, ICompanyRepository companyRepository)
    {
        _repository = repository;
        _companyRepository = companyRepository;
    }

    public async Task<string> CreateCustomerAsync(CustomerUserCreationModel model)
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

        await _repository.CreateAsync(entity);

        return "User created successfully";
    }

    public async Task<string> CreateCompanyUserAsync(CompanyUserCreationModel model)
    {
        // Check if company with this CrNumber already exists
        var companyExists = await _companyRepository.ExistsByCrNumberAsync(model.CrNumber);
        if (companyExists)
        {
            return "Company already exists. Please contact with the owner or support center";
        }

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

        await _repository.CreateAsync(user);

        return "Company registered successfully";
    }

    public async Task<User?> LoginAsync(LoginModel model)
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

        return user;
    }
}
