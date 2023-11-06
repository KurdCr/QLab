using QLab.Helpers.Exceptions;
using QLab.Database.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using QLab.Database;

namespace QLab.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _permission;

    public AuthorizeAttribute(string permission = "")
    {
        _permission = permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous) return;

        var roleId = context.HttpContext.Items["RoleId"];
        if (roleId == null)
            throw new UnauthorizedException();

        var db = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

        if (!ValidatePermission(Convert.ToInt32(roleId), db))
            throw new ForbiddenException();
    }

    private bool ValidatePermission(int? roleId, ApplicationDbContext? db)
    {
        if (roleId == null || db == null) return false;

        var roleExist = db.Roles.Any(x => x.Id == roleId);
        if (!roleExist) return false;

        if (string.IsNullOrWhiteSpace(_permission)) return true;

        return db.RolePermissions
            .Any(x => x.RoleId == roleId && x.Permission == _permission);
    }
}