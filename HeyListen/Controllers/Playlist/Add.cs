using Discord.WebSocket;
using HeyListen.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyListen.Controllers.Playlist
{
    class Add
    {
        public static async Task PerformAsync(ISocketMessageChannel channel, SpotifyWebAPI spotify, User dj, string query)
        {
            var profile = spotify.GetPrivateProfile();
            var userId = spotify.GetPrivateProfile().Id;

            var search_result = spotify.SearchItems(query, SpotifyAPI.Web.Enums.SearchType.Track, limit: 5)?.Tracks;

            if (search_result.Items.Count == 1)
                await AddTrack(channel, spotify, search_result.Items[0], dj, userId);
            else if (search_result.Items.Count > 1)
                await SearchResults(channel, spotify, search_result.Items, userId);
            else
                await channel.SendMessageAsync(text: "No Tracks Found");
        }

        private static async Task AddTrack(ISocketMessageChannel channel, SpotifyWebAPI spotify, FullTrack track, User dj, string id)
        {
            spotify.AddPlaylistTrack(id, dj.Playlist, track.Uri);
            await channel.SendMessageAsync(text: $"Track Added: {track.Name}");
        }

        private static async Task SearchResults(ISocketMessageChannel channel, SpotifyWebAPI spotify, List<FullTrack> track, string id)
        {
            await channel.SendMessageAsync(text: "Multiple Tracks Found - to add them, use the command at the bottom of each listing:");

            track.ForEach(async t => await channel.SendMessageAsync("", embed: Views.Spotify.Track.Response(t, true)));
        }
    }
}
