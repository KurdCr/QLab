using Xunit;
using Moq;
using QLab.Services;
using QLab.Database.Repositories;
using QLab.Database.Models;
using System.Threading.Tasks;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserById_ReturnsUser()
    {
        var userId = 1;
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(new User { Id = userId, Username = "John" });
        var userService = new UserService(mockRepository.Object);

        var user = await userService.GetUserAsync(userId);

        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
        Assert.Equal("John", user.Username);
    }

    [Fact]
    public async Task GetUserById_UserNotFound_ReturnsNull()
    {
        var userId = 2;
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);
        var userService = new UserService(mockRepository.Object);

        var user = await userService.GetUserAsync(userId);

        Assert.Null(user);
    }

    [Fact]
    public async Task CreateUser_ValidUser_CreatesUser()
    {
        var newUser = new User { Id = 3, Username = "Alice" };
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.CreateAsync(newUser)).ReturnsAsync(newUser);
        var userService = new UserService(mockRepository.Object);

        var createdUser = await userService.CreateUserAsync(newUser);

        Assert.NotNull(createdUser);
        Assert.Equal(newUser.Id, createdUser.Id);
        Assert.Equal(newUser.Username, createdUser.Username);
    }

    [Fact]
    public async Task CreateUser_NullUser_ReturnsNull()
    {
        User newUser = null;
        var mockRepository = new Mock<IUserRepository>();
        var userService = new UserService(mockRepository.Object);

        var createdUser = await userService.CreateUserAsync(newUser);

        Assert.Null(createdUser);
    }

    [Fact]
    public async Task UpdateUser_ValidUser_UpdatesUser()
    {
        var userId = 1;
        var updatedUser = new User { Id = userId, Username = "UpdatedName" };
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.UpdateAsync(updatedUser)).Verifiable();
        mockRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(updatedUser);
        var userService = new UserService(mockRepository.Object);

        await userService.UpdateUserAsync(userId, updatedUser);

        mockRepository.Verify(repo => repo.UpdateAsync(updatedUser), Times.Once);
    }


    [Fact]
    public async Task DeleteUser_UserFound_DeletesUser()
    {
        var userId = 1;
        var userToDelete = new User { Id = userId, Username = "ToDelete" };
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(userToDelete);
        mockRepository.Setup(repo => repo.DeleteAsync(userToDelete)).Verifiable();
        var userService = new UserService(mockRepository.Object);

        await userService.DeleteUserAsync(userId);

        mockRepository.Verify(repo => repo.DeleteAsync(userToDelete), Times.Once);
    }
}
