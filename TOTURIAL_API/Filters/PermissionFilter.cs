using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTURIAL_API.Filters
{
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var effectivePolicy = WsPolicy;
            if (effectivePolicy == null)
            {
                effectivePolicy = await AuthorizationPolicy.CombineAsync(PolicyProvider, AuthorizeData);
            }

            if (effectivePolicy == null)
            {
                return;
            }

            // Build a ClaimsPrincipal with the Policy's required authentication types
            if (effectivePolicy.AuthenticationSchemes != null && effectivePolicy.AuthenticationSchemes.Count > 0)
            {
                ClaimsPrincipal newPrincipal = null;
                for (var i = 0; i < effectivePolicy.AuthenticationSchemes.Count; i++)
                {
                    var scheme = effectivePolicy.AuthenticationSchemes[i];
                    var result = await context.HttpContext.Authentication.AuthenticateAsync(scheme);
                    if (result != null)
                    {
                        newPrincipal = SecurityHelper.MergeUserPrincipal(newPrincipal, result);
                    }
                }
                // If all schemes failed authentication, provide a default identity anyways
                if (newPrincipal == null)
                {
                    newPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
                }
                context.HttpContext.User = newPrincipal;
            }

            // Allow Anonymous skips all authorization
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            var httpContext = context.HttpContext;
            var authService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            // Note: Default Anonymous User is new ClaimsPrincipal(new ClaimsIdentity())
            if (!await authService.AuthorizeAsync(httpContext.User, context, effectivePolicy))
            {
                context.Result = new ChallengeResult(effectivePolicy.AuthenticationSchemes.ToArray());
            }
        }
    }
}
