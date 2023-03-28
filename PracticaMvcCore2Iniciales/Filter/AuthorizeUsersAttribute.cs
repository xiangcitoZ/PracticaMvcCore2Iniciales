using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PracticaMvcCore2Iniciales.Filter
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute,
        IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string controller =
                context.RouteData.Values["controller"].ToString();
            string action =
                context.RouteData.Values["action"].ToString();

            ITempDataProvider provider =
                context.HttpContext.RequestServices
                .GetService<ITempDataProvider>();

            var TempData = provider.LoadTempData(context.HttpContext);

            TempData["controller"] = controller;
            TempData["action"] = action;

            provider.SaveTempData(context.HttpContext, TempData);
            var user = context.HttpContext.User;
            if(user.Identity.IsAuthenticated == false) 
            {
                RouteValueDictionary rutaLogin =
                    new RouteValueDictionary(
                            new { controller = "Login", action = "Index" }
                        );
                context.Result =
                    new RedirectToRouteResult(rutaLogin);
            }
        }
    }
}
