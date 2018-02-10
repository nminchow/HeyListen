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
            await PlaybackBase.MessageBeforeAfter(channel, spotify, Skip);
        }

        private static void Skip(ISocketMessageChannel channel, SpotifyWebAPI spotify)
        {
            spotify.SkipPlaybackToPrevious();
        }
    }
}
