using System.Linq;
using HeyListen.Models;
using Discord.WebSocket;

namespace HeyListen.Controllers.Admin
{
    static class SetDj
    {
        public static async System.Threading.Tasks.Task PerformAsync(SocketUser user, ISocketMessageChannel channel, DataBase db)
        {
            // check admin permission here
            
            var dbChannel = db.Channels.FirstOrDefault(c => c.DiscordId == channel.Id.ToString());
            if (dbChannel == null)
            {
                dbChannel = new Channel { DiscordId = channel.Id.ToString() };
                db.Channels.Add(dbChannel);
            }

            dbChannel.CurrentDj = Helpers.FindOrCreateUser.Perform(user, db);
            db.SaveChanges();

            // request token if not configured
            if (string.IsNullOrEmpty(dbChannel.CurrentDj.SpotifyToken))
            {
                await Discord.UserExtensions.SendMessageAsync(user, text: "", embed: Views.Admin.AuthUrl.Response());
            }

            await channel.SendMessageAsync(text: $"{user.Username} is now the DJ!");
        }
    }
}
