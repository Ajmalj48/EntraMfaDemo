using System.Web.Mvc;
using System.Linq;
using System.Security.Claims;

namespace EntraMfaDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Secure()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claims = identity?.Claims.ToList();

            ViewBag.Name =
                claims?.FirstOrDefault(c => c.Type == "name")?.Value;

            ViewBag.Username =
                claims?.FirstOrDefault(c =>
                    c.Type == "preferred_username")?.Value;

            ViewBag.ObjectId =
                claims?.FirstOrDefault(c => c.Type == "oid")?.Value;

            ViewBag.TenantId =
                claims?.FirstOrDefault(c => c.Type == "tid")?.Value;

            ViewBag.AuthenticationMethod =
                claims?.FirstOrDefault(c => c.Type == "amr")?.Value;

            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}