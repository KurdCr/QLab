using Microsoft.EntityFrameworkCore;
using QLab.Database.Models;

namespace QLab.Database.Repositories
{
    public interface IRolePermissionRepository : IBaseRepository<RolePermission, int>
    {
        Task<RolePermission> GetByNameAsync(string permissionName);
        Task<RolePermission> GetByNameAndRoleIdAsync(string permissionName, int roleId);
    }

    public class RolePermissionRepository : BaseRepository<RolePermission, int>, IRolePermissionRepository
    {
        public RolePermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RolePermission> GetByNameAsync(string permissionName)
        {
            return await DbContext.RolePermissions.FirstOrDefaultAsync(x => x.Permission == permissionName);
        }
        public async Task<RolePermission> GetByNameAndRoleIdAsync(string permissionName, int roleId)
        {
            return await DbContext.RolePermissions.FirstOrDefaultAsync(x => x.Permission == permissionName && x.RoleId == roleId);
        }
    }
}
