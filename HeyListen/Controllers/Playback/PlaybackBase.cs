using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Controllers.Helpers;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeyListen.Controllers.Playback
{
    public class PlaybackBase
    {
        public static async Task PerformAsync(Func<ISocketMessageChannel, SpotifyWebAPI, Task> task, ISocketMessageChannel channel, DataBase db, AdminOrchestrator config)
        {
            var dbChannel = db.Channels.Include(c => c.CurrentDj).FirstOrDefault(c => c.DiscordId == channel.Id.ToString());

            var spotify = SpotifyAuth.Perform(dbChannel.CurrentDj, db, config);

            await task(channel, spotify);
        }
    }
}
