using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace WebUI.ActionFilters;

public class MyAuthorizeAttribute : ActionFilterAttribute
{
    public string? Roles { get; set; }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (Roles != null)
            {
                var roles = context.HttpContext.User.Claims.Where(r => r.Type == ClaimTypes.Role)
                    .Select(claim => claim.Value).ToList();
                foreach (var role in roles)
                {
                    if (!Roles.Contains(role))
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new RedirectResult("/Auth/Login", true);
                        break;
                    }
                }
            }
        }
        else
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new RedirectResult("/Auth/Login", true);
        }
    }
}
