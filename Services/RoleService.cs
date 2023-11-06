using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Helpers.Exceptions;
using QLab.Services.Contracts;
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> GetRoleAsync(int id)
    {
        return await _roleRepository.GetByIdAsync(id);
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        return (await _roleRepository.GetAllAsync()).ToList();
    }

    public async Task<Role> CreateRoleAsync(Role role)
    {
        return await _roleRepository.CreateAsync(role);
    }

    public async Task UpdateRoleAsync(int id, Role updatedRole)
    {
        var existingRole = await _roleRepository.GetByIdAsync(id);
        if (existingRole == null)
        {
            throw new NotFoundException("Role not found.");
        }

        existingRole.Name = updatedRole.Name;

        await _roleRepository.UpdateAsync(existingRole);
    }

    public async Task DeleteRoleAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
        {
            throw new NotFoundException("Role not found.");
        }

        await _roleRepository.DeleteAsync(role);
    }
}
