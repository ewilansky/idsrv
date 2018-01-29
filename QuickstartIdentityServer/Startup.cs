using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());


                services.AddAuthentication() //options => {
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //})

                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "172986759996-f031j0mlqh8k9qgumm3h602cm5rt0595.apps.googleusercontent.com";
                    options.ClientSecret = "R4Rd84vx1Gab7cJo-bJhXl6v";
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, "Azure Portal", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "415ee7f3-0c53-4c58-9579-3cf54c1fd63e";
                    // options.ClientSecret = "7KyCzTicUzgDuAL5wf/JGdM23bSEnCyBuwAwK6IxN3w=";
                    options.Authority = $"https://login.microsoftonline.com/common";
                    options.ResponseType = OpenIdConnectResponseType.IdToken;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://sts.windows.net/8d327499-ca18-4d3b-b150-24dfd1cbf5f5/",
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}