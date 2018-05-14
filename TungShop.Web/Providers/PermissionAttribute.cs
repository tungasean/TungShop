using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using TungShop.Common.Enums;
using TungShop.Web.Models;

namespace TungShop.Web.Providers
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string Function;
        public string Action;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                var roles = JsonConvert.DeserializeObject<List<string>>(principal.FindFirst("roles").Value);
                if (roles.Count > 0)
                {
                    if (!roles.Contains(RoleEnum.Admin.ToString()))
                    {
                        
                    }
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
            }
        }
    }
}