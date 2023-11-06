using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QLab.Helpers.Constants;
using QLab.Helpers.Exceptions;
using QLab.Services.Contracts;
using Xunit;

public class RolePermissionControllerTests
{
    [Fact]
    public async Task AssignPermissionToRole_ValidRequest_ReturnsNoContent()
    {
        var roleId = 1;
        var permissionName = "TestPermission";
        var rolePermissionServiceMock = new Mock<IRolePermissionService>();
        rolePermissionServiceMock.Setup(service => service.AssignPermissionToRoleAsync(roleId, permissionName));
        var controller = new RolePermissionController(rolePermissionServiceMock.Object);

        var result = await controller.AssignPermissionToRole(roleId, permissionName);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task AssignPermissionToRole_RolePermissionServiceThrowsNotFoundException_ReturnsNotFound()
    {
        var roleId = 1;
        var permissionName = "TestPermission";
        var rolePermissionServiceMock = new Mock<IRolePermissionService>();
        rolePermissionServiceMock.Setup(service => service.AssignPermissionToRoleAsync(roleId, permissionName)).ThrowsAsync(new NotFoundException("Role or permission not found"));
        var controller = new RolePermissionController(rolePermissionServiceMock.Object);

        var result = await controller.AssignPermissionToRole(roleId, permissionName);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task AssignPermissionToRole_RolePermissionServiceThrowsException_ReturnsBadRequest()
    {
        var roleId = 1;
        var permissionName = "TestPermission";
        var rolePermissionServiceMock = new Mock<IRolePermissionService>();
        rolePermissionServiceMock.Setup(service => service.AssignPermissionToRoleAsync(roleId, permissionName)).ThrowsAsync(new Exception("Some error"));
        var controller = new RolePermissionController(rolePermissionServiceMock.Object);

        var result = await controller.AssignPermissionToRole(roleId, permissionName);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task RevokePermissionFromRole_ValidRequest_ReturnsNoContent()
    {
        var roleId = 1;
        var permissionName = "TestPermission";
        var rolePermissionServiceMock = new Mock<IRolePermissionService>();
        rolePermissionServiceMock.Setup(service => service.RevokePermissionFromRoleAsync(roleId, permissionName));
        var controller = new RolePermissionController(rolePermissionServiceMock.Object);

        var result = await controller.RevokePermissionFromRole(roleId, permissionName);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task RevokePermissionFromRole_RolePermissionServiceThrowsNotFoundException_ReturnsNotFound()
    {
        var roleId = 1;
        var permissionName = "TestPermission";
        var rolePermissionServiceMock = new Mock<IRolePermissionService>();
        rolePermissionServiceMock.Setup(service => service.RevokePermissionFromRoleAsync(roleId, permissionName)).ThrowsAsync(new NotFoundException("Role or permission not found"));
        var controller = new RolePermissionController(rolePermissionServiceMock.Object);

        var result = await controller.RevokePermissionFromRole(roleId, permissionName);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task RevokePermissionFromRole_RolePermissionServiceThrowsException_ReturnsBadRequest()
    {
        var roleId = 1;
        var permissionName = "TestPermission";
        var rolePermissionServiceMock = new Mock<IRolePermissionService>();
        rolePermissionServiceMock.Setup(service => service.RevokePermissionFromRoleAsync(roleId, permissionName)).ThrowsAsync(new Exception("Some error"));
        var controller = new RolePermissionController(rolePermissionServiceMock.Object);

        var result = await controller.RevokePermissionFromRole(roleId, permissionName);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
}
