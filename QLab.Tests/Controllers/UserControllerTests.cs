using Microsoft.AspNetCore.Mvc;
using Moq;
using QLab.Database.Models;
using QLab.Helpers.Exceptions;

public class UserControllerTests
{
    [Fact]
    public async Task Get_ValidUser_ReturnsOkResult()
    {
        int userId = 1;
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetUserAsync(userId)).ReturnsAsync(new User { Id = userId, Username = "User1" });
        var controller = new UserController(mockUserService.Object);

        var result = await controller.Get(userId);

        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        Assert.IsType<User>(okResult.Value);
        var user = (User)okResult.Value;
        Assert.Equal(userId, user.Id);
    }

    [Fact]
    public async Task Get_UserNotFound_ReturnsNotFoundResult()
    {
        int userId = 1;
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetUserAsync(userId)).ReturnsAsync((User)null);
        var controller = new UserController(mockUserService.Object);

        var result = await controller.Get(userId);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult()
    {
        var users = new List<User> { new User { Id = 1, Username = "User1" }, new User { Id = 2, Username = "User2" } };
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);
        var controller = new UserController(mockUserService.Object);

        var result = await controller.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        Assert.IsType<List<User>>(okResult.Value);
        var resultUsers = (List<User>)okResult.Value;
        Assert.Equal(2, resultUsers.Count);
    }

    [Fact]
    public async Task Create_ValidUser_ReturnsCreatedAtAction()
    {
        var newUser = new User { Username = "NewUser" };
        var createdUser = new User { Id = 3, Username = newUser.Username };
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.CreateUserAsync(newUser)).ReturnsAsync(createdUser);
        var controller = new UserController(mockUserService.Object);

        var result = await controller.Create(newUser);

        Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdAtAction = (CreatedAtActionResult)result.Result;
        Assert.Equal("Get", createdAtAction.ActionName);
        Assert.Equal(createdUser.Id, createdAtAction.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ValidUser_ReturnsNoContent()
    {
        int userId = 1;
        var updatedUser = new User { Id = userId, Username = "UpdatedUser" };
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.UpdateUserAsync(userId, updatedUser)).Returns(Task.CompletedTask);
        var controller = new UserController(mockUserService.Object);

        var result = await controller.Update(userId, updatedUser);

        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task Update_UserNotFound_ReturnsNotFoundResult()
    {
        int userId = 1;
        var updatedUser = new User { Id = userId, Username = "UpdatedUser" };
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.UpdateUserAsync(userId, updatedUser)).ThrowsAsync(new NotFoundException("User not found."));
        var controller = new UserController(mockUserService.Object);

        var result = await controller.Update(userId, updatedUser);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task Delete_ValidUser_ReturnsNoContent()
    {
        int userId = 1;
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.DeleteUserAsync(userId)).Returns(Task.CompletedTask);
        var controller = new UserController(mockUserService.Object);

        var result = await controller.Delete(userId);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_UserNotFound_ReturnsNotFoundResult()
    {
        int userId = 1;
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.DeleteUserAsync(userId)).ThrowsAsync(new NotFoundException("User not found."));
        var controller = new UserController(mockUserService.Object);

        var result = await controller.Delete(userId);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
