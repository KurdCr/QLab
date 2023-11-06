using Microsoft.EntityFrameworkCore;
using QLab.Database.Models;

namespace QLab.Database.Repositories
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task<User> GetUserByUsernameAsync(string username);
    }

    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await DbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await DbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

    }
}
