﻿using System;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HeyListen.Preconditions
{
    public class RequirePlaylistControl : PreconditionAttribute
    {
        public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            // possible to pass the context in?
            var db = (DataBase)services.GetService(typeof(DataBase));
            var dbUser = db.Channels.Include(c => c.CurrentDj).FirstOrDefault(c => c.DiscordId == context.Channel.Id.ToString())?.CurrentDj;
            if (dbUser == null)
                return PreconditionResult.FromError("No DJ has been set for this channel. Set one up with '!hey dj {username}'");
            if(dbUser.DiscordId == context.Message.Author.Id.ToString() || dbUser.AllowPlaylistControl)
                return PreconditionResult.FromSuccess();
            return PreconditionResult.FromError("Current DJ does not allow third party playlist control. In order to enable, dj must enter: '!hey allow playlist true'");
        }
    }
}
