using CMSv2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CMSv2.Handler
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,
    Inherited = true, AllowMultiple = true)]
    public class BasicAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            try
            {
                // validation comes in here
                if (!actionContext.Request.Headers.Contains("ApiKey"))
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    string key = actionContext.Request.Headers.GetValues("ApiKey").FirstOrDefault();

                    if (string.IsNullOrEmpty(key) == false && key != SharedClass.getApiKey())
                    {
                        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                    
                }
            }
            catch (System.Exception ex)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}