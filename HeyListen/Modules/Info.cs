using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeyListen.Controllers.Info;

namespace HeyListen.Modules
{
    [Group("hey")]
    public class Info : ModuleBase<SocketCommandContext>
    {

        private DataBase _database;
        private AdminOrchestrator _adminOrchestator;

        public Info(DataBase database, AdminOrchestrator admin)
        {
            _database = database;
            _adminOrchestator = admin;
        }

        [Command("playing", RunMode = RunMode.Async)]
        [Summary("get info about playing song")]
        public async Task GetSong()
        {
            await Playing.PerformAsync(Context.Channel, _database, _adminOrchestator);
        }

        // ~sample square 20 -> 400
        [Command("help", RunMode = RunMode.Async)]
        [Summary("get command overview")]
        public async Task GetInfo()
        {
            var view = Views.Info.Help.Response();
            await Context.Channel.SendMessageAsync(view.Item1, embed: view.Item2);
        }
        
    }
}
