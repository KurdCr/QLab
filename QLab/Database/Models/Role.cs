
using System.ComponentModel.DataAnnotations;

namespace QLab.Database.Models;

public class Role
{
    public Role()
    {
        Permissions = new List<RolePermission>();
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<RolePermission> Permissions { get; set; }
}
