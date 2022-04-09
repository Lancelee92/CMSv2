using CMSv1.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CMSv1.Handler
{
    public class ValidateHashAuthenticationSchemeOptions : AuthenticationSchemeOptions
    { }

    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ValidateHashAuthenticationSchemeOptions>
    {
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ValidateHashAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // validation comes in here
            if (!Request.Headers.ContainsKey("ApiKey"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Not Found."));
            }

            try
            {
                string key = Request.Headers["ApiKey"].ToString();

                if (string.IsNullOrEmpty(key) == false)
                {
                    if(key == AppSettings.Current.Key)
                    {
                        // generate claimsIdentity on the name of the class
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(nameof(ApiKeyAuthenticationHandler));

                        // generate AuthenticationTicket from the Identity
                        // and current authentication scheme
                        AuthenticationTicket ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

                        return Task.FromResult(AuthenticateResult.Success(ticket));
                    }                    
                }                

            }
            catch (System.Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail("ApiKeyParseException"));
            }

            return Task.FromResult(AuthenticateResult.Fail("Model is Empty"));
        }
    }

    public static class AuthenticationSchemeConstants
    {
        public const string ValidateHashAuthenticationScheme = "ValidateHashAuthenticationScheme";
    }
}
