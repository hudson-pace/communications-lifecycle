using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Okta.AspNetCore;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IDbContextFactory<BlazorWebApp.Data.BlazorWebAppContext> _dbContextFactory;
        public AccountController(IDbContextFactory<BlazorWebApp.Data.BlazorWebAppContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IActionResult> SignIn([FromQuery] string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(OktaDefaults.MvcAuthenticationScheme);
            }

            var sub = User.FindFirst("sub");
            await GetUserProfile(sub.ToString());
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }

        // Get User Profile if it exists, else add one.
        private async Task GetUserProfile(string sub)
        {
            using var context = _dbContextFactory.CreateDbContext();
            UserProfile? UserProfile = await context.UserProfile.FirstOrDefaultAsync(u => u.Sub == sub);
            if (UserProfile is null)
            {
                UserProfile = new UserProfile { Sub = sub };
                context.UserProfile.Add(UserProfile);
                await context.SaveChangesAsync();
            }
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