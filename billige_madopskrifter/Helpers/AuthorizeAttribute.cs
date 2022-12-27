using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using billige_madopskrifter.Model;

namespace billige_madopskrifter.Helpers
{
    // Lavet med hjælp fra projektet https://github.com/Ahaubro/Wemuda-book-app 
    public class AuthorizeAttribute
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class AutherizeAttribute : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = (User)context.HttpContext.Items["User"];
                if (user == null)
                {
                    // not logged in
                    context.Result = new JsonResult(new
                    {
                        message = "Unauthorized"
                    })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
            }
        }
    }
}
