using System;
using System.Threading.Tasks;
using System.Linq;
using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Services.Contracts;
using QLab.Helpers.Exceptions;
using QLab.Helpers.Constants;

public class RolePermissionService : IRolePermissionService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public RolePermissionService(IRoleRepository roleRepository, IRolePermissionRepository rolePermissionRepository)
    {
        _roleRepository = roleRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task AssignPermissionToRoleAsync(int roleId, string permissionName)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new NotFoundException("Role not found.");
        }

        var permission = await _rolePermissionRepository.GetByNameAndRoleIdAsync(permissionName, roleId);
        if (permission != null)
        {
            throw new NotFoundException("Role already has permission.");
        }

        if (!role.Permissions.Any(p => p.Permission == permissionName))
        {
            await _rolePermissionRepository.CreateAsync(new RolePermission { RoleId = role.Id, Permission = permissionName });
        }
    }

    public async Task RevokePermissionFromRoleAsync(int roleId, string permissionName)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new NotFoundException("Role not found.");
        }

        var permission = await _rolePermissionRepository.GetByNameAndRoleIdAsync(permissionName, roleId);
        if (permission == null)
        {
            throw new NotFoundException("Permission not found.");
        }

        if (role.Name == "Administrator")
        {
            throw new Exception("Can not revoke from Administrator role");
        }
        await _rolePermissionRepository.DeleteAsync(permission);
    }
}
