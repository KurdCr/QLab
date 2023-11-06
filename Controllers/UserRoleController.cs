using Microsoft.AspNetCore.Mvc;
using QLab.Helpers.Attributes;
using QLab.Helpers.Constants;
using QLab.Helpers.Exceptions;
using QLab.Services.Contracts;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("user-role")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [Authorize(PermissionConstants.AssignRoleToUser)]
    [HttpPost("{userId}/assign/{roleId}")]
    public async Task<IActionResult> AssignRoleToUser(int userId, int roleId)
    {
        try
        {
            await _userRoleService.AssignRoleToUserAsync(userId, roleId);
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

    [Authorize(PermissionConstants.RevokeRoleFromUser)]
    [HttpDelete("{userId}/revoke/{roleId}")]
    public async Task<IActionResult> RevokeRoleFromUser(int userId, int roleId)
    {
        try
        {
            await _userRoleService.RevokeRoleFromUserAsync(userId);
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
