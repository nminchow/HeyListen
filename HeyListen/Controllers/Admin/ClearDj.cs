using System.Linq;
using HeyListen.Models;
using Discord.WebSocket;

namespace HeyListen.Controllers.Admin
{
    static class ClearDj
    {
        public static async System.Threading.Tasks.Task PerformAsync(ISocketMessageChannel channel, DataBase db)
        {

            var dbChannel = db.Channels.FirstOrDefault(c => c.DiscordId == channel.Id.ToString());
            if (dbChannel == null)
            {
                dbChannel = new Channel { DiscordId = channel.Id.ToString() };
                db.Channels.Add(dbChannel);
            }

            dbChannel.CurrentDj = null;
            db.SaveChanges();

            await channel.SendMessageAsync(text: $"The DJ has been cleared.");
        }
    }
}
