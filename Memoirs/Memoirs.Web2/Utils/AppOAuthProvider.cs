using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Memoirs.Common.EntityFramework;
using Memoirs.Common.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Ninject;

namespace Memoirs.Web2.Utils
{
    public class AppOAuthProvider:IOAuthAuthorizationServerProvider
    {
        public AppOAuthProvider()
        {
            OnMatchEndpoint = context => Task.FromResult<object>(null);
            //OnValidateClientRedirectUri = context => Task.FromResult<object>(null);
            //OnValidateClientAuthentication = context => Task.FromResult<object>(null);

            //OnValidateAuthorizeRequest = DefaultBehavior.ValidateAuthorizeRequest;
            //OnValidateTokenRequest = DefaultBehavior.ValidateTokenRequest;

            //OnGrantAuthorizationCode = DefaultBehavior.GrantAuthorizationCode;
            //OnGrantResourceOwnerCredentials = context => Task.FromResult<object>(null);
            //OnGrantRefreshToken = DefaultBehavior.GrantRefreshToken;
            //OnGrantClientCredentials = context => Task.FromResult<object>(null);
            //OnGrantCustomExtension = context => Task.FromResult<object>(null);

            //OnAuthorizeEndpoint = context => Task.FromResult<object>(null);
            //OnTokenEndpoint = context => Task.FromResult<object>(null);
        }

        public Func<OAuthMatchEndpointContext, Task> OnMatchEndpoint { get; set; }

        public Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            return this.OnMatchEndpoint(context);
        }

        public Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            throw new NotImplementedException();
        }

        public Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            throw new NotImplementedException();
        }

        public async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = NinjectWebCommon.Bootstrapper.Kernel.Get<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var ticket = AuthUtil.AuthUserWithToken(user, context.Request.Context.Authentication, userManager);
            context.Validated(ticket);
        }
        

        public Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            throw new NotImplementedException();
        }

        public Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            throw new NotImplementedException();
        }

        public Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            throw new NotImplementedException();
        }

        public async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
        }
    }
}