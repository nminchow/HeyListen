using System.Linq;
using HeyListen.Models;
using Discord.WebSocket;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyListen.Controllers.Playback
{
    class SkipTrack
    {
        public static async System.Threading.Tasks.Task PerformAsync(ISocketMessageChannel channel, SpotifyWebAPI spotify)
        {
            var playing = spotify.GetPlayingTrack();
            await channel.SendMessageAsync("Skipping:", embed: Views.Spotify.Track.Response(playing.Item));
            var result = spotify.SkipPlaybackToNext();
            await channel.SendMessageAsync("Now Playing:", embed: Views.Spotify.Track.Response(spotify.GetPlayingTrack().Item));
        }
    }
}
