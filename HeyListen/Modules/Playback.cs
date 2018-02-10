using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Controllers.Playback;
using HeyListen.Preconditions;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeyListen.Modules
{
    [Group("hey")]
    [RequirePlaybackControl]
    public class Playback : ModuleBase<SocketCommandContext>
    {
        private DataBase _database;
        private AdminOrchestrator _adminOrchestator;


        public Playback(DataBase database, AdminOrchestrator admin)
        {
            _database = database;
            _adminOrchestator = admin;
        }

        [Command("skip", RunMode = RunMode.Async)]
        public async Task SkipAsync()
        {
            await Call(SkipTrack.PerformAsync);
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task PlayAsync()
        {
            await Call(Play.PerformAsync);
        }

        [Command("pause", RunMode = RunMode.Async)]
        [CurrentlyDisabled]
        public async Task PauseAsync()
        {
            await Call(Pause.PerformAsync);
        }

        [Command("back", RunMode = RunMode.Async)]
        public async Task BackAsync()
        {
            await Call(BackTrack.PerformAsync);
        }

        private async Task Call(Func<ISocketMessageChannel, SpotifyWebAPI, Task> task)
        {
            await PlaybackBase.PerformAsync(task, Context.Channel, _database, _adminOrchestator);
        }
    }
}
