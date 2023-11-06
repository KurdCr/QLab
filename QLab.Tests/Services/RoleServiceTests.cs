using Xunit;
using Moq;
using QLab.Services;
using QLab.Database.Repositories;
using QLab.Database.Models;
using System.Threading.Tasks;

public class RoleServiceTests
{
    [Fact]
    public async Task GetRoleById_ReturnsRole()
    {
        var roleId = 1;
        var mockRepository = new Mock<IRoleRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync(new Role { Id = roleId, Name = "Admin" });
        var roleService = new RoleService(mockRepository.Object);

        var role = await roleService.GetRoleAsync(roleId);

        Assert.NotNull(role);
        Assert.Equal(roleId, role.Id);
        Assert.Equal("Admin", role.Name);
    }

    [Fact]
    public async Task GetRoleById_RoleNotFound_ReturnsNull()
    {
        var roleId = 2;
        var mockRepository = new Mock<IRoleRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync((Role)null);
        var roleService = new RoleService(mockRepository.Object);

        var role = await roleService.GetRoleAsync(roleId);

        Assert.Null(role);
    }

    [Fact]
    public async Task CreateRole_ValidRole_CreatesRole()
    {
        var newRole = new Role { Id = 3, Name = "Moderator" };
        var mockRepository = new Mock<IRoleRepository>();
        mockRepository.Setup(repo => repo.CreateAsync(newRole)).ReturnsAsync(newRole);
        var roleService = new RoleService(mockRepository.Object);

        var createdRole = await roleService.CreateRoleAsync(newRole);

        Assert.NotNull(createdRole);
        Assert.Equal(newRole.Id, createdRole.Id);
        Assert.Equal(newRole.Name, createdRole.Name);
    }

    [Fact]
    public async Task CreateRole_NullRole_ReturnsNull()
    {
        Role newRole = null;
        var mockRepository = new Mock<IRoleRepository>();
        var roleService = new RoleService(mockRepository.Object);

        var createdRole = await roleService.CreateRoleAsync(newRole);

        Assert.Null(createdRole);
    }

    [Fact]
    public async Task UpdateRole_ValidRole_UpdatesRole()
    {
        var roleId = 1;
        var updatedRole = new Role { Id = roleId, Name = "UpdatedRole" };
        var mockRepository = new Mock<IRoleRepository>();
        mockRepository.Setup(repo => repo.UpdateAsync(updatedRole)).Verifiable();
        mockRepository.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync(updatedRole);
        var roleService = new RoleService(mockRepository.Object);

        await roleService.UpdateRoleAsync(roleId, updatedRole);

        mockRepository.Verify(repo => repo.UpdateAsync(updatedRole), Times.Once);
    }


    [Fact]
    public async Task DeleteRole_RoleFound_DeletesRole()
    {
        var roleId = 1;
        var roleToDelete = new Role { Id = roleId, Name = "ToDelete" };
        var mockRepository = new Mock<IRoleRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync(roleToDelete);
        mockRepository.Setup(repo => repo.DeleteAsync(roleToDelete)).Verifiable();
        var roleService = new RoleService(mockRepository.Object);

        await roleService.DeleteRoleAsync(roleId);

        mockRepository.Verify(repo => repo.DeleteAsync(roleToDelete), Times.Once);
    }
}
