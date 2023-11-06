using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QLab.Database.Models;

namespace QLab.Services.Contracts
{
    public interface IRoleService
    {
        Task<Role> GetRoleAsync(int id);
        Task<List<Role>> GetAllRolesAsync();
        Task<Role> CreateRoleAsync(Role role);
        Task UpdateRoleAsync(int id, Role updatedRole);
        Task DeleteRoleAsync(int id);
    }
}
