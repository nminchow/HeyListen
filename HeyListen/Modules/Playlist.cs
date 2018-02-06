using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Controllers.Playback;
using HeyListen.Controllers.Playlist;
using HeyListen.Models;
using HeyListen.Preconditions;
using SpotifyAPI.Web;
using System;
using System.Threading.Tasks;

namespace HeyListen.Modules
{
    [Group("hey")]
    [RequirePlaylistControl]
    [RequirePlaylistSet]
    public class Playlist : ModuleBase<SocketCommandContext>
    {
        private DataBase _database;
        private AdminOrchestrator _adminOrchestator;

        public Playlist(DataBase database, AdminOrchestrator admin)
        {
            _database = database;
            _adminOrchestator = admin;
        }

        [Command("add", RunMode = RunMode.Async)]
        public async Task AddTrackAsync([Remainder] string query)
        {
            await Call(Add.PerformAsync, query);
        }

        [Command("add_uri", RunMode = RunMode.Async)]
        public async Task AddTrackUriAsync([Remainder] string query)
        {
            await Call(AddUri.PerformAsync, query);
        }


        private async Task Call(Func<ISocketMessageChannel, SpotifyWebAPI, User, string, Task> task, string query)
        {
            await PlaylistBase.PerformAsync(task, Context.Channel, _database, query, _adminOrchestator);
        }
    }
}
