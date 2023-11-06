using Microsoft.AspNetCore.Mvc;
using QLab.Helpers.Exceptions;
using QLab.Helpers.Attributes;
using QLab.Helpers.Constants;
using QLab.Services.Contracts;

[ApiController]
[Route("role-permissions")]
public class RolePermissionController : ControllerBase
{
    private readonly IRolePermissionService _rolePermissionService;

    public RolePermissionController(IRolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
    }

    [Authorize(PermissionConstants.AssignPermissionToRole)]
    [HttpPost("{roleId}/assign/{permissionName}")]
    public async Task<IActionResult> AssignPermissionToRole(int roleId, string permissionName)
    {
        try
        {
            await _rolePermissionService.AssignPermissionToRoleAsync(roleId, permissionName);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(PermissionConstants.RevokePermissionFromRole)]
    [HttpDelete("{roleId}/revoke/{permissionName}")]
    public async Task<IActionResult> RevokePermissionFromRole(int roleId, string permissionName)
    {
        try
        {
            await _rolePermissionService.RevokePermissionFromRoleAsync(roleId, permissionName);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
