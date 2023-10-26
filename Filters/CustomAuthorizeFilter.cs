using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace examplemvc.Filters;

    public class CustomAuthorizeFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            if (context.HttpContext.Session.GetString("user") == null)
            {
                var con = (Controller)context.Controller;
                context.Result = con.BadRequest(new { msg = "User hasn't logged in yet" });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

