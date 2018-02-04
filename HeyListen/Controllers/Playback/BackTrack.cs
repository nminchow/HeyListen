using System.Linq;
using HeyListen.Models;
using Discord.WebSocket;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyListen.Controllers.Playback
{
    class BackTrack
    {
        public static async System.Threading.Tasks.Task PerformAsync(ISocketMessageChannel channel, SpotifyWebAPI spotify)
        {
            var playing = spotify.GetPlayingTrack();
            await channel.SendMessageAsync(text: $"Skipping Back: {playing.Item.Name}");
            var result = spotify.SkipPlaybackToPrevious();
            await channel.SendMessageAsync(text: $"Now Playing: {spotify.GetPlayingTrack().Item.Name}");
        }
    }
}
