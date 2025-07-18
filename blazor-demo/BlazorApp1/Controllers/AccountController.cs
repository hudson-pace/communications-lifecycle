using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Okta.AspNetCore;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Services;

namespace BlazorApp1.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IDbContextFactory<BlazorWebApp.Data.BlazorWebAppContext> _dbContextFactory;
        private readonly IUserProfileService _userProfileService;
        public AccountController(IDbContextFactory<BlazorWebApp.Data.BlazorWebAppContext> dbContextFactory, IUserProfileService userProfileService)
        {
            _dbContextFactory = dbContextFactory;
            _userProfileService = userProfileService;
        }
        public async Task<IActionResult> SignIn([FromQuery] string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(OktaDefaults.MvcAuthenticationScheme);
            }
            await _userProfileService.GetAsync();
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
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