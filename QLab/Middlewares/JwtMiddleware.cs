﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QLab.Helpers.Constants;

namespace QLab.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            var (userId, roleId) = ValidateAccessToken(token);
            context.Items["UserId"] = userId;
            context.Items["RoleId"] = roleId;
        }

        await _next(context);
    }

    private (string? userId, string? roleId) ValidateAccessToken(string accessToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Secret));

            tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = key,
                ValidIssuer = JwtConstant.Issuer,
                ValidAudience = JwtConstant.Audience,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;

            var userId = jwtToken?.Claims.FirstOrDefault(x => x.Type == JwtConstant.UserId)?.Value;
            var roleId = jwtToken?.Claims.FirstOrDefault(x => x.Type == JwtConstant.RoleId)?.Value;
            return (userId, roleId);
        }
        catch
        {
            return (null, null);
        }
        return (null, null);
    }
}