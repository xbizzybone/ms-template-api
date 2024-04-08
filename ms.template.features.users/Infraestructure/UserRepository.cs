using Microsoft.Extensions.Configuration;
using ms.template.features.users.Application.Interfaces;
using ms.template.features.users.Domain;

namespace ms.template.features.users.Infraestructure;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;

    public UserRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<User> CreateUserAsync(User user)
    {

        return Task.FromResult(new User { Id = Guid.NewGuid(), Name = user.Name, Email = user.Email, Password = user.Password, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
    }

    public Task<User> DeleteUserAsync(Guid id)
    {
        
        return Task.FromResult(new User { Id = id });
    }

    public Task<User> GetUserByIdAsync(Guid id)
    {
        
        return Task.FromResult(new User { Id = id });
    }

    public Task<List<User>> GetUsersAsync()
    {
        
        return Task.FromResult(new List<User>());
    }

    public Task<User> UpdateUserAsync(User user)
    {
        
        return Task.FromResult(new User { Id = user.Id, Name = user.Name, Email = user.Email, Password = user.Password, CreatedAt = user.CreatedAt, UpdatedAt = DateTime.Now });
    }
}
