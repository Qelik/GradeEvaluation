using System.Web.Mvc;

public class IdAuthorizeAttribute : ActionFilterAttribute
{
    public string Role { get; set; }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var session = filterContext.HttpContext.Session;

        if (session == null || session["UserId"] == null)
        {
            RedirectToLogin(filterContext);
            return;
        }

        if (!string.IsNullOrEmpty(Role))
        {
            var userRole = session["UserRole"] as string;
            if (userRole != Role)
            {
                RedirectToLogin(filterContext);
            }
        }
    }

    private void RedirectToLogin(ActionExecutingContext context)
    {
        var routeValues = new System.Web.Routing.RouteValueDictionary
        {
            { "controller", "Account" },
            { "action", "Login" }
        };

        // Prefer the Role configured on the attribute so Login can render the proper view.
        if (!string.IsNullOrEmpty(Role))
        {
            routeValues["role"] = Role;
        }
        else
        {
            // Try to preserve any role from current route or querystring if present
            if (context.RouteData.Values.ContainsKey("role"))
            {
                routeValues["role"] = context.RouteData.Values["role"];
            }
            else if (context.HttpContext.Request.QueryString["role"] != null)
            {
                routeValues["role"] = context.HttpContext.Request.QueryString["role"];
            }
        }

        context.Result = new RedirectToRouteResult(routeValues);
    }
}