using QLab.Database.Models;
using QLab.Helpers.Constants;

namespace QLab.Database.Repositories;

public static class DataInitializer
{
    public static void Run(ApplicationDbContext db)
    {
        if (db.Users.Any()) return;

        // create role
        var adminRole = new Role
        {
            Name = "Administrator"
        };
        db.Roles.Add(adminRole);

        var user = new Role
        {
            Name = "User"
        };
        db.Roles.Add(user);

        db.SaveChanges();


        // create permissions
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.ReadUser });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.ReadUsers });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.CreateUser });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.UpdateUser });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.DeleteUser });

        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.ReadRole });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.ReadRole });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.CreateRole });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.UpdateRole });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.DeleteRole });

        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.AssignPermissionToRole });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.RevokePermissionFromRole });

        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.AssignRoleToUser });
        db.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, Permission = PermissionConstants.RevokeRoleFromUser });


        db.SaveChanges();


        // create user
        var balen = new User
        {
            Username = "Balen",
            Password = "Balen",
            RoleId = adminRole.Id
        };
        db.Users.Add(balen);

        var balen1 = new User
        {
            Username = "Balen1",
            Password = "Balen1",
            RoleId = user.Id
        };
        db.Users.Add(balen1);

        db.SaveChanges();
    }
}