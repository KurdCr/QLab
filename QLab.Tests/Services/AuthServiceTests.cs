using Moq;
using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Helpers.Exceptions;
using QLab.Helpers.Resources;
using QLab.Services.Contracts;

public class AuthServiceTests
{


    [Fact]
    public async Task RegisterUser_ValidRequest_ReturnsNewUser()
    {
        var authServiceMock = new Mock<IAuthService>();
        authServiceMock.Setup(auth => auth.RegisterUser(It.IsAny<RegistrationRequest>()))
            .ReturnsAsync(new User
            {
                Id = 1,
                Username = "testuser",
                RoleId = 2
            });

        var authService = authServiceMock.Object;

        var registrationRequest = new RegistrationRequest("testuser", "testpassword");
        var result = await authService.RegisterUser(registrationRequest);

        Assert.NotNull(result);
        Assert.Equal(registrationRequest.Username, result.Username);
    }

    [Fact]
    public async Task LoginUser_ValidRequest_ReturnsUser()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var roleRepositoryMock = new Mock<IRoleRepository>();
        var loginRequest = new LoginRequest("testuser", "testpassword");
        var existingUser = new User
        {
            Id = 1,
            Username = "testuser",
            Password = "testpassword",
            RoleId = 2
        };
        userRepositoryMock.Setup(r => r.GetUserByUsernameAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(existingUser);
        var authService = new AuthService(userRepositoryMock.Object, roleRepositoryMock.Object);

        var result = await authService.LoginUser(loginRequest);

        Assert.Equal(loginRequest.Username, result.Username);
    }
}
