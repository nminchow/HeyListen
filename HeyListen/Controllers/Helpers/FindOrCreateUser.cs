using Discord.WebSocket;
using HeyListen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeyListen.Controllers.Helpers
{
    class FindOrCreateUser
    {
        public static User Perform(SocketUser user, DataBase db)
        {
            var dbUser = db.Users.FirstOrDefault(u => u.DiscordId == user.Id.ToString());
            if (dbUser == null)
            {
                dbUser = new User { DiscordId = user.Id.ToString()};
                db.Users.Add(dbUser);
            }
            return dbUser;
        }
    }
}
