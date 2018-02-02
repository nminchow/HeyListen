using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeyListen.Modules
{
    public class Info : ModuleBase<SocketCommandContext>
    {
        [Group("hey")]
        public class Sample : ModuleBase<SocketCommandContext>
        {

            public Sample() { }

            // ~sample square 20 -> 400
            [Command("playing", RunMode = RunMode.Async)]
            [Summary("get info about playing song")]
            public async Task GetInfo()
            {
                await ReplyAsync(message: "", embed: Views.Info.Playing.Response());
            }
        }
    }
}
