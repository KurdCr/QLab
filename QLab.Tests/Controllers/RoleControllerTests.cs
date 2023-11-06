using Microsoft.AspNetCore.Mvc;
using Moq;
using QLab.Database.Models;
using QLab.Helpers.Exceptions;
using QLab.Services.Contracts;

public class RoleControllerTests
{
    [Fact]
    public async Task Get_ValidRole_ReturnsOkResult()
    {
        int roleId = 1;
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.GetRoleAsync(roleId)).ReturnsAsync(new Role { Id = roleId, Name = "RoleName" });
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.Get(roleId);

        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        Assert.IsType<Role>(okResult.Value);
        var role = (Role)okResult.Value;
        Assert.Equal(roleId, role.Id);
    }

    [Fact]
    public async Task Get_RoleNotFound_ReturnsNotFoundResult()
    {
        int roleId = 1;
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.GetRoleAsync(roleId)).ReturnsAsync((Role)null);
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.Get(roleId);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult()
    {
        var roles = new List<Role> { new Role { Id = 1, Name = "Role1" }, new Role { Id = 2, Name = "Role2" } };
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.GetAllRolesAsync()).ReturnsAsync(roles);
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        Assert.IsType<List<Role>>(okResult.Value);
        var resultRoles = (List<Role>)okResult.Value;
        Assert.Equal(2, resultRoles.Count);
    }

    [Fact]
    public async Task Create_ValidRole_ReturnsCreatedAtAction()
    {
        var newRole = new Role { Name = "NewRole" };
        var createdRole = new Role { Id = 3, Name = newRole.Name };
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.CreateRoleAsync(newRole)).ReturnsAsync(createdRole);
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.Create(newRole);

        Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdAtAction = (CreatedAtActionResult)result.Result;
        Assert.Equal("Get", createdAtAction.ActionName);
        Assert.Equal(createdRole.Id, createdAtAction.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ValidRole_ReturnsOkResult()
    {
        int roleId = 1;
        var updatedRole = new Role { Id = roleId, Name = "UpdatedRole" };
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.UpdateRoleAsync(roleId, updatedRole)).Returns(Task.CompletedTask);
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.Update(roleId, updatedRole);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_RoleNotFound_ReturnsNotFoundResult()
    {
        int roleId = 1;
        var updatedRole = new Role { Id = roleId, Name = "UpdatedRole" };
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.UpdateRoleAsync(roleId, updatedRole)).ThrowsAsync(new NotFoundException("Role not found."));
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.Update(roleId, updatedRole);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ValidRole_ReturnsOkResult()
    {
        int roleId = 1;
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.DeleteRoleAsync(roleId)).Returns(Task.CompletedTask);
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.Delete(roleId);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_RoleNotFound_ReturnsNotFoundResult()
    {
        int roleId = 1;
        var mockRoleService = new Mock<IRoleService>();
        mockRoleService.Setup(service => service.DeleteRoleAsync(roleId)).ThrowsAsync(new NotFoundException("Role not found."));
        var controller = new RoleController(mockRoleService.Object);

        var result = await controller.Delete(roleId);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
