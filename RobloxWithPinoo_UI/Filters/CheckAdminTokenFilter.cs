using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Filters
{
    public class CheckAdminTokenFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "" });
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null || (jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value != "Admin"))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "" });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}