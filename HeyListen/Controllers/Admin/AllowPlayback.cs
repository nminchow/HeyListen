using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Controllers.Admin
{
    class AllowPlayback
    {
        public static async System.Threading.Tasks.Task PerformAsync(SocketUser user, bool allowPlayback, DataBase db)
        {
            var currentUser = Helpers.FindOrCreateUser.Perform(user, db);
            currentUser.AllowPlaybackControl = allowPlayback;
            db.SaveChanges();

            var result_text = allowPlayback ? "allowed" : "disallowed";
            await Discord.UserExtensions.SendMessageAsync(user, text: $"Public playback control {result_text}.");
        }
    }
}
