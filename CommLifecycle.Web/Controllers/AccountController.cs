using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Okta.AspNetCore;
using Microsoft.EntityFrameworkCore;
using CommLifecycle.Web.Services;
using System.Security.Claims;

namespace CommLifecycle.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        public async Task<IActionResult> SignIn([FromQuery] string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(OktaDefaults.MvcAuthenticationScheme);
            }
            else
            {
                var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                Console.WriteLine("ROLES ROLES ROLES");
                foreach (var role in roles)
                {
                    Console.WriteLine($"User has role: {role}");
                }
                Console.WriteLine("CLAIMS");
                Console.WriteLine(User.IsInRole("admin") ? "ADMIN" : "NOT");
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                }
            }
            return LocalRedirect(returnUrl ?? Url.Content("~/Communications"));
        }
        public IActionResult SignOut([FromQuery] string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            return new SignOutResult(
                new[]
                {
                    OktaDefaults.MvcAuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                },
                new AuthenticationProperties { RedirectUri = Url.Content("~/") }
            );
        }
    }
}