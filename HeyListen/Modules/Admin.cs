using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Preconditions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeyListen.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        private DataBase _database;
        private AdminOrchestrator _adminOrchestator;

        public Admin(DataBase database, AdminOrchestrator admin)
        {
            _database = database;
            _adminOrchestator = admin;
        }
       
        [Command("dj", RunMode = RunMode.Async)]
        [Summary("Set DJ Access")]
        [RequireUserPermission(ChannelPermission.ManageChannel)]
        public async Task SetDjAsync([Summary("Set the dj")] SocketUser user = null)
        {
            var u = user ?? Context.Message.Author;
            await SetDj.PerformAsync(u, Context.Channel, _database);
        }

        [Command("clear", RunMode = RunMode.Async)]
        [Summary("Clear DJ")]
        [RequireUserPermission(ChannelPermission.ManageChannel)]
        public async Task ClearDjAsync([Summary("Clear the dj")] SocketUser user = null)
        {
            await ClearDj.PerformAsync(Context.Channel, _database);
        }

        [Command("playlist")]
        [Summary("Switch to new playlist")]
        [RequireDj]
        public async Task SwitchToNewPlaylist()
        {
            await SwithToNewPlaylist.PerformAsync(Context.Message.Author, Context.Channel, _database, _adminOrchestator);
        }

        [Command("playlist")]
        [Summary("Switch to existing playlist")]
        [RequireDj]
        public async Task SwitchToExistingPlaylist([Remainder] string searchTerm)
        {
            await SwithToExistingPlaylist.PerformAsync(Context.Message.Author, Context.Channel, _database, _adminOrchestator, searchTerm);
        }

        [Command("token", RunMode = RunMode.Async)]
        [Summary("Set Spotify Token")]
        public async Task SetToken(string token)
        {
            await Controllers.Admin.SetToken.PerformAsync(Context.Message.Author, token, _database, _adminOrchestator);
        }

        [Command("allow playback", RunMode = RunMode.Async)]
        public async Task AllowPlayback(bool allow)
        {
            await Controllers.Admin.AllowPlayback.PerformAsync(Context.Message.Author, allow, _database);
        }

        [Command("allow playlist", RunMode = RunMode.Async)]
        public async Task AllowPlaylist(bool allow)
        {
            await Controllers.Admin.AllowPlaylist.PerformAsync(Context.Message.Author, allow, _database);
        }
    }
}
