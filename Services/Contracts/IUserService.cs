using QLab.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<User> GetUserAsync(int id);
    Task<List<User>> GetAllUsersAsync();
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(int id, User updatedUser);
    Task DeleteUserAsync(int id);
}
