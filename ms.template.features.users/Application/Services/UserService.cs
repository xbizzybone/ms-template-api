using AutoMapper;
using ms.template.features.users.Application.DTOs;
using ms.template.features.users.Application.Interfaces;
using ms.template.features.users.Application.Schemas;
using ms.template.features.users.Domain;

namespace ms.template.features.users.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponse> CreateUserAsync(UserDTO request)
    {
        var user = _mapper.Map<User>(request);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.CreateUserAsync(user);

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<List<UserResponse>> GetUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();

        return _mapper.Map<List<UserResponse>>(users);
    }

    public async Task<UserResponse> UpdateUserAsync(Guid id, UserDTO request)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        user.Name = request.Name;
        user.Email = request.Email;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateUserAsync(user);

        return _mapper.Map<UserResponse>(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        await _userRepository.DeleteUserAsync(id);
    }

}
