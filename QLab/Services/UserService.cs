using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Helpers.Exceptions;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return (List<User>)await _userRepository.GetAllAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task UpdateUserAsync(int id, User updatedUser)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new NotFoundException("User not found.");
        }

        existingUser.Username = updatedUser.Username;

        await _userRepository.UpdateAsync(existingUser);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        await _userRepository.DeleteAsync(user);
    }
}
