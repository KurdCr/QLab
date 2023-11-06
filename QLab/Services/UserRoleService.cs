using System;
using System.Threading.Tasks;
using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Services.Contracts;
using QLab.Helpers.Exceptions;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserRoleService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task AssignRoleToUserAsync(int userId, int roleId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new NotFoundException("Role not found.");
        }

        user.Role = role;
        await _userRepository.UpdateAsync(user);
    }

    public async Task RevokeRoleFromUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        user.Role = null;
        await _userRepository.UpdateAsync(user);
    }
}
