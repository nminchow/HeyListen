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
        //private readonly DataBase _database;
        //public RequirePlaybackControl(DataBase database)
        //{
        //    _database = database;
        //}
        public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            // possible to pass the context in?
            var dbUser = new DataBase().Channels.Include(c => c.CurrentDj).FirstOrDefault(c => c.DiscordId == context.Channel.Id.ToString())?.CurrentDj;
            if (dbUser == null)
                return PreconditionResult.FromError("No DJ has been set for this channel. Set one up with '!hey dj {username}'");
            if(dbUser.DiscordId == context.Message.Author.Id.ToString() || dbUser.AllowPlaylistControl)
                return PreconditionResult.FromSuccess();
            return PreconditionResult.FromError("Current DJ does not allow third party playback control.");
        }
    }
}
