using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TravelHubAgency.Filters
{
    public class AuthorizeUsuarioAttribute : AuthorizeAttribute,
        IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var usuario = context.HttpContext.User;

            /* con esto hacemos que la navegacion se dinámica es decir,
             que redirigimos al usuario a la ventana que había seleccionado
             */
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();

            ITempDataProvider provider =
                context.HttpContext.RequestServices
                .GetService<ITempDataProvider>();

            var tempData = provider.LoadTempData(context.HttpContext);
            tempData["controller"] = controller;
            tempData["action"] = action;

            provider.SaveTempData(context.HttpContext, tempData);

            if (usuario.Identity.IsAuthenticated == false)
            {
                context.Result = this.GetRoute("Managed", "Auth");
            }
        }

        public RedirectToRouteResult GetRoute(string controller, string action)
        {
            RouteValueDictionary route =
                new RouteValueDictionary(
                new
                {
                    controller = controller,
                    action = action
                });

            RedirectToRouteResult result =
                new RedirectToRouteResult(route);
            return result;
        }
    }
}
