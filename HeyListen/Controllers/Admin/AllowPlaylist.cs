using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Controllers.Admin
{
    class AllowPlaylist
    {
        public static async System.Threading.Tasks.Task PerformAsync(SocketUser user, bool allowPlayback, DataBase db)
        {
            var currentUser = Helpers.FindOrCreateUser.Perform(user, db);
            currentUser.AllowPlaylistControl = allowPlayback;
            db.SaveChanges();

            var result_text = allowPlayback ? "allowed" : "disallowed";
            await Discord.UserExtensions.SendMessageAsync(user, text: $"Public playlist control {result_text}.");
        }
    }
}
