using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Web.Mvc;

namespace EntraMfaDemo.Controllers
{
    public class AccountController : Controller
    {
        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext()
                    .Authentication
                    .Challenge(
                        new AuthenticationProperties
                        {
                            RedirectUri = "/Home/Secure"
                        },
                        OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext()
                .Authentication
                .SignOut(
                    CookieAuthenticationDefaults.AuthenticationType,
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);

            return new HttpStatusCodeResult(200);
        }
    }
}