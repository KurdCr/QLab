using Microsoft.IdentityModel.Tokens;
using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Helpers.Constants;
using QLab.Helpers.Exceptions;
using QLab.Helpers.Resources;
using QLab.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AuthService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<User> RegisterUser(RegistrationRequest request)
    {
        var existingUser = _userRepository.GetUserByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            throw new BadRequestException("Username already in use.");
        }

        var userRole = await _roleRepository.GetRoleByNameAsync("User");

        if (userRole == null)
        {
            userRole = new Role { Name = "User" };
            await _roleRepository.CreateAsync(userRole);
        }

        var newUser = new User
        {
            Username = request.Username,
            Password = request.Password,
            RoleId = userRole.Id
        };

        await _userRepository.CreateAsync(newUser);

        return newUser;
    }

    public async Task<User> LoginUser(LoginRequest request)
    {
        var user = await _userRepository.GetUserByUsernameAndPasswordAsync(request.Username, request.Password);
        if (user == null)
        {
            throw new BadRequestException("User not found.");
        }
        return user;
    }

    public string GenerateAccessToken(int userId, int roleId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Secret));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = JwtConstant.Issuer,
            Audience = JwtConstant.Audience,
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtConstant.UserId, userId.ToString()),
                new Claim(JwtConstant.RoleId, roleId.ToString())
            }),
            Expires = DateTime.Now.AddMinutes(JwtConstant.ExpiryMinutes),
            SigningCredentials = credential
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
