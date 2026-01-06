using HajjSystem.Models.Entities;

namespace HajjSystem.Data.Repositories;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}
