using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Controllers.Helpers;
using HeyListen.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeyListen.Controllers.Playlist
{
    public class PlaylistBase
    {
        public static async Task PerformAsync(Func<ISocketMessageChannel, SpotifyWebAPI, User, string, Task> task, ISocketMessageChannel channel, DataBase db, string query, AdminOrchestrator config)
        {
            var dbChannel = db.Channels.Include(c => c.CurrentDj).FirstOrDefault(c => c.DiscordId == channel.Id.ToString());

            var spotify = SpotifyAuth.Perform(dbChannel.CurrentDj, db, config);

            await task(channel, spotify, dbChannel.CurrentDj, query);
        }
    }
}
