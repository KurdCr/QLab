using System.Threading.Tasks;

public interface IUserRoleService
{
    Task AssignRoleToUserAsync(int userId, int roleId);
    Task RevokeRoleFromUserAsync(int userId);
}
