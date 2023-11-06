
using System.ComponentModel.DataAnnotations;

namespace QLab.Database.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public int? RoleId { get; set; }
}