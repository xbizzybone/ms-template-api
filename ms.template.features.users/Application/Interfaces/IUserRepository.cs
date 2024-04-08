using ms.template.features.users.Domain;

namespace ms.template.features.users.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<User> DeleteUserAsync(Guid id);
}
