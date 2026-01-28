using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class AuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User =>
        _httpContextAccessor.HttpContext?.User ??
        throw new UnauthorizedAccessException("No hay usuario autenticado.");

    public int GetUserId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                      ?? User.FindFirst(JwtRegisteredClaimNames.Sub);

        if (idClaim == null)
            throw new UnauthorizedAccessException("Token sin ID de usuario.");

        return int.Parse(idClaim.Value);
    }

    public int? GetUserIdOrNull()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                      ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (idClaim == null)
            return null;

        if (int.TryParse(idClaim.Value, out int userId))
            return userId;

        return null;
    }

    public string GetUserRole()
    {
        var roleClaim = User.FindFirst(ClaimTypes.Role);

        if (roleClaim == null)
            throw new UnauthorizedAccessException("Token sin rol asignado.");

        return roleClaim.Value;
    }

    public string? GetEmail()
    {
        return User.FindFirst(ClaimTypes.Email)?.Value ??
               User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
    }
}
