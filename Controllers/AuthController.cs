using Microsoft.AspNetCore.Mvc;
using QLab.Helpers.Attributes;
using QLab.Helpers.Resources;
using QLab.Services.Contracts;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegistrationRequest request)
    {
        var newUser = await _authService.RegisterUser(request);
        var accessToken = _authService.GenerateAccessToken(newUser.Id, newUser.RoleId.GetValueOrDefault());
        var response = new RegistrationResponse(newUser.Username, "Registration successful", accessToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.LoginUser(request);
        var accessToken = _authService.GenerateAccessToken(user.Id, user.RoleId.GetValueOrDefault());
        var response = new LoginResponse(accessToken, user.Id, user.RoleId, user.Username);
        return Ok(response);
    }
}
