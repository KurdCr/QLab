using QLab.Database.Models;
using QLab.Helpers.Resources;
using System.Threading.Tasks;

namespace QLab.Services.Contracts
{
    public interface IAuthService
    {
        Task<User> RegisterUser(RegistrationRequest request);
        Task<User> LoginUser(LoginRequest request);
        string GenerateAccessToken(int userId, int roleId);
    }
}
