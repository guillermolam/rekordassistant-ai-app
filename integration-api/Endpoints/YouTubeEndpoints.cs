using Google.Apis.YouTube.v3;

public static class YouTubeEndpoints
{
    public static void MapYouTubeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/youtube").RequireAuthorization();

        group.MapGet("/playlists", async (YouTubeService youTubeService) =>
        {
            var request = youTubeService.Playlists.List("snippet");
            request.Mine = true;
            return await request.ExecuteAsync();
        });

        group.MapGet("/playlists/{playlistId}/tracks", async (string playlistId, YouTubeService youTubeService) =>
        {
            var request = youTubeService.PlaylistItems.List("snippet");
            request.PlaylistId = playlistId;
            return await request.ExecuteAsync();
        });
    }
}