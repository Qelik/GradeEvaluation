using System.Web.Mvc;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsLoggedIn()
        {
            return Session["UserId"] != null;
        }

        protected ActionResult RedirectIfNotAuthorized()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account"); // redirect to login page
            }
            return null;
        }
    }
}