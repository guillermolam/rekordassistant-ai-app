using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.Hosting;

public static class AuthenticationExtensions
{
    public static TBuilder AddGoogleAuthentication<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Google:ClientId"]!;
            options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
            options.Scope.Add("https://www.googleapis.com/auth/youtube.readonly");

            // For Blazor WASM token forwarding
            options.SaveTokens = true;
        });

        return builder;
    }

    public static TBuilder AddYouTubeServices<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddScoped<YouTubeService>(provider =>
        {
            var accessToken = provider.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?
                .GetTokenAsync("access_token")
                .Result;

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromAccessToken(accessToken),
                ApplicationName = "DJ Assistant"
            });
        });

        return builder;
    }
}