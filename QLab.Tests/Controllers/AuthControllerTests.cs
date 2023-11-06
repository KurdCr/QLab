using Microsoft.AspNetCore.Mvc;
using Moq;
using QLab.Database.Models;
using QLab.Helpers.Resources;
using QLab.Services.Contracts;
using Xunit;

public class AuthControllerTests
{
    [Fact]
    public async void Register_ValidRegistration_ReturnsOkResult()
    {
        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.RegisterUser(It.IsAny<RegistrationRequest>()).Result)
            .Returns(new User { Id = 1, RoleId = 2, Username = "TestUser" });

        var controller = new AuthController(authService.Object);
        var registrationRequest = new RegistrationRequest("TestUser", "Password123");

        var result = await controller.Register(registrationRequest);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<RegistrationResponse>(okResult.Value);
        Assert.Equal("Registration successful", response.Message);
        Assert.Equal("TestUser", response.Username);
    }

    [Fact]
    public async void Login_ValidLogin_ReturnsOkResult()
    {
        var authService = new Mock<IAuthService>();
        authService.Setup(a => a.LoginUser(It.IsAny<LoginRequest>()).Result)
            .Returns(new User { Id = 1, RoleId = 2, Username = "TestUser"});

        var controller = new AuthController(authService.Object);
        var loginRequest = new LoginRequest("TestUser", "Password123");

        var result = await controller.Login(loginRequest);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.Equal("TestUser", response.Username);
    }
}
