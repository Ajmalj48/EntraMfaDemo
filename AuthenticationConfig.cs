using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace EntraMfaDemo
{
    public static class AuthenticationConfig
    {
        public static void Configure(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(
                CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType =
                        CookieAuthenticationDefaults.AuthenticationType
                });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId =
                        ConfigurationManager.AppSettings["ClientId"],

                    Authority =
                        "https://login.microsoftonline.com/" +
                        ConfigurationManager.AppSettings["TenantId"] +
                        "/v2.0",

                    RedirectUri =
                        ConfigurationManager.AppSettings["RedirectUri"],

                    PostLogoutRedirectUri =
                        ConfigurationManager.AppSettings[
                            "PostLogoutRedirectUri"],

                    ResponseType = OpenIdConnectResponseType.IdToken,

                    Scope = "openid profile",

                    TokenValidationParameters =
                        new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true,

                            NameClaimType = "name"
                        },

                    Notifications =
                        new OpenIdConnectAuthenticationNotifications
                        {
                            AuthenticationFailed = context =>
                            {
                                context.HandleResponse();

                                context.Response.Redirect(
                                    "/Home/Error?message=" +
                                    Uri.EscapeDataString(
                                        context.Exception.Message));

                                return Task.FromResult(0);
                            },

                            SecurityTokenValidated = context =>
                            {
                                return Task.FromResult(0);
                            }
                        }
                });
        }
    }
}