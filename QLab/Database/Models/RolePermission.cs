
using System.ComponentModel.DataAnnotations;

namespace QLab.Database.Models;

public class RolePermission
{
    [Key]
    public int Id { get; set; }
    public Role Role { get; set; }
    public int? RoleId { get; set; }
    public string Permission { get; set; }
}

