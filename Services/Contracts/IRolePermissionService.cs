using System.Threading.Tasks;

namespace QLab.Services.Contracts
{
    public interface IRolePermissionService
    {
        Task AssignPermissionToRoleAsync(int roleId, string permissionName);
        Task RevokePermissionFromRoleAsync(int roleId, string permissionName);
    }
}
