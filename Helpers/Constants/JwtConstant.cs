namespace QLab.Helpers.Constants;

public static class JwtConstant
{
    public const string Secret = "BalenAssignmentqlab";
    public const string Issuer = "https://balen.tech";
    public const string Audience = "https://balen.tech";
    public const int ExpiryMinutes = 60;
    public const string UserId = "uid";
    public const string RoleId = "rid";
}