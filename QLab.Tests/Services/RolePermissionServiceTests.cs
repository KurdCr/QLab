using System.Threading.Tasks;
using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Services.Contracts;
using QLab.Helpers.Exceptions;
using Moq;
using Xunit;

public class RolePermissionServiceTests
{
    [Fact]
    public async Task AssignPermissionToRoleAsync_RoleNotFound_ThrowsNotFoundException()
    {
        var roleId = 1;
        var permissionName = "ReadUser";
        var roleRepositoryMock = new Mock<IRoleRepository>();
        roleRepositoryMock.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync((Role)null);

        var rolePermissionRepositoryMock = new Mock<IRolePermissionRepository>();
        var rolePermissionService = new RolePermissionService(roleRepositoryMock.Object, rolePermissionRepositoryMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => rolePermissionService.AssignPermissionToRoleAsync(roleId, permissionName));
    }

    [Fact]
    public async Task AssignPermissionToRoleAsync_PermissionNotFound_CreatesNewPermission()
    {
        var roleId = 1;
        var permissionName = "ReadUser";
        var role = new Role { Id = roleId };
        var roleRepositoryMock = new Mock<IRoleRepository>();
        roleRepositoryMock.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync(role);

        var rolePermissionRepositoryMock = new Mock<IRolePermissionRepository>();
        rolePermissionRepositoryMock.Setup(repo => repo.GetByNameAsync(permissionName)).ReturnsAsync((RolePermission)null);

        var rolePermissionService = new RolePermissionService(roleRepositoryMock.Object, rolePermissionRepositoryMock.Object);

        await rolePermissionService.AssignPermissionToRoleAsync(roleId, permissionName);

        rolePermissionRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<RolePermission>()), Times.Once);
    }

}
