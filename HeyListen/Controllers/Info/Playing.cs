using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Controllers.Helpers;
using HeyListen.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyListen.Controllers.Info
{
    class Playing
    {
        public static async Task PerformAsync(ISocketMessageChannel channel, DataBase db, AdminOrchestrator config)
        {
            var dj = db.Channels.Include(c => c.CurrentDj).FirstOrDefault(c => c.DiscordId == channel.Id.ToString())?.CurrentDj;
            var spotify = await SpotifyAuth.PerformAsync(dj, db, config, channel);
            await channel.SendMessageAsync("Now Playing:", embed: Views.Spotify.Track.Response(spotify.GetPlayingTrack().Item));
        }
    }
}
