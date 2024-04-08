using ms.template.features.users.Application.DTOs;
using ms.template.features.users.Application.Schemas;

namespace ms.template.features.users.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(UserDTO request);
    Task<UserResponse> GetUserAsync(Guid id);
    Task<List<UserResponse>> GetUsersAsync();
    Task<UserResponse> UpdateUserAsync(Guid id, UserDTO request);
    Task DeleteUserAsync(Guid id);
}
