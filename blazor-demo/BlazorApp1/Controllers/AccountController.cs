using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Okta.AspNetCore;

namespace BlazorApp1.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        public IActionResult SignIn([FromQuery] string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(OktaDefaults.MvcAuthenticationScheme);
            }
            // return RedirectToAction("Index", "Home");
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }

        public IActionResult SignOut()
        {
            return new SignOutResult(
                new[]
                {
                    OktaDefaults.MvcAuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                },
                new AuthenticationProperties { RedirectUri = "/" }
            );
        }

    }
}
