using Google.Apis.YouTube.v3.Data;

namespace rekordassistant-web.Services
{
    public class YouTubeClientService
    {
        private readonly HttpClient httpClient;

        public YouTubeClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PlaylistListResponse?> GetUserPlaylists()
        {
            var response = await httpClient.GetFromJsonAsync<PlaylistListResponse>("/youtube/playlists");
            return response ?? new PlaylistListResponse();
        }

        public async Task<PlaylistItemListResponse?> GetPlaylistTracks(string playlistId)
        {
            var response = await httpClient.GetFromJsonAsync<PlaylistItemListResponse>($"/youtube/playlists/{playlistId}/tracks");
            return response ?? new PlaylistItemListResponse();
        }
    }
}
