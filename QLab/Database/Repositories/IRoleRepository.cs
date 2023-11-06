using Microsoft.EntityFrameworkCore;
using QLab.Database.Models;

namespace QLab.Database.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role, int>
    {
        Task<Role> GetRoleByNameAsync(string roleName);
    }

    public class RoleRepository : BaseRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await DbContext.Roles.FirstOrDefaultAsync(x => x.Name == roleName);
        }

    }
}
