namespace QLab.Helpers.Resources;

public record LoginResponse(
    string AccessToken,
    int? UserId,
    int? RoleId,
    string Username);