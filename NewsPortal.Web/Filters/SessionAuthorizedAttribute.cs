
namespace NewsPortal.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SessionAuthorizedAttribute : ActionFilterAttribute
{
    private readonly string _sessionKey;

    public SessionAuthorizedAttribute(string sessionKey = "UserId")
    {
        _sessionKey = sessionKey;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session;

        if (session == null || string.IsNullOrEmpty(session.GetString(_sessionKey)))
        {
            // Redirect to Login page if session is missing
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }
    }
}
