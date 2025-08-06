using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

public class RoleClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;
        if (identity == null) return Task.FromResult(principal);

        // Find all 'roles' claims and convert them to ClaimTypes.Role
        var roleClaims = identity.FindAll("roles").ToList();

        foreach (var roleClaim in roleClaims)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
        }

        return Task.FromResult(principal);
    }
}