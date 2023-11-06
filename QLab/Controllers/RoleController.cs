using Microsoft.AspNetCore.Mvc;
using QLab.Helpers.Attributes;
using QLab.Database.Models;
using QLab.Helpers.Constants;
using QLab.Services.Contracts;
using QLab.Helpers.Exceptions;

[ApiController]
[Route("roles")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }


    [Authorize(PermissionConstants.ReadRole)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> Get(int id)
    {
        try
        {
            var role = await _roleService.GetRoleAsync(id);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Authorize(PermissionConstants.ReadRoles)]
    [HttpGet]
    public async Task<ActionResult<List<Role>>> GetAll()
    {
        try
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(PermissionConstants.CreateRole)]
    [HttpPost]
    public async Task<ActionResult<Role>> Create([FromBody] Role role)
    {
        try
        {
            var createdRole = await _roleService.CreateRoleAsync(role);
            return CreatedAtAction(nameof(Get), new { id = createdRole.Id }, createdRole);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize(PermissionConstants.UpdateRole)]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Role updatedRole)
    {
        try
        {
            await _roleService.UpdateRoleAsync(id, updatedRole);
            return Ok("Role updated successfully");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize(PermissionConstants.DeleteRole)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _roleService.DeleteRoleAsync(id);
            return Ok("Role deleted successfully");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
