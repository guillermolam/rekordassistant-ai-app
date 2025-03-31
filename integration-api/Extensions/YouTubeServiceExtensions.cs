using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Authentication;

public static class YouTubeServiceExtensions
{
    public static IServiceCollection AddYouTubeApi(this IServiceCollection services)
    {
        services.AddScoped<YouTubeService>(provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var token = httpContextAccessor.HttpContext?.GetTokenAsync("access_token").Result;

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromAccessToken(token),
                ApplicationName = "DJ Assistant"
            });
        });

        return services;
    }
}