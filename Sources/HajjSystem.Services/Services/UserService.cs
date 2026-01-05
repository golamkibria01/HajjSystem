using HajjSystem.Data.Repositories;
using HajjSystem.Models.Entities;

namespace HajjSystem.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _repository.GetByUsernameAsync(username);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _repository.GetByEmailAsync(email);
    }

    public async Task<User> CreateAsync(User user)
    {
        return await _repository.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _repository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
