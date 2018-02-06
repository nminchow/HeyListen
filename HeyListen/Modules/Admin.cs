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
    [Group("hey")]
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

        [Command("playlist")]
        [Summary("Switch to playlist mode")]
        [RequireDj]
        public async Task SwitchToPlaylist()
        {
            await SwithToPlaylist.PerformAsync(Context.Message.Author, Context.Channel, _database, _adminOrchestator);
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
