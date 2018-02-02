using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeyListen.Modules
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        [Group("hey")]
        public class Sample : ModuleBase<SocketCommandContext>
        {

            public Sample() { }

            // ~sample square 20 -> 400
            [Command("dj", RunMode = RunMode.Async)]
            [Summary("Request DJ Access")]
            public async Task SetDjAsync([Summary("Set the dj")] SocketUser user = null)
            {
                var u = user ?? Context.Message.Author;
                await Discord.UserExtensions.SendMessageAsync(u, text: "", embed: Views.Admin.AuthUrl.Response());
            }

            // ~sample square 20 -> 400
            [Command("square", RunMode = RunMode.Async)]
            [Summary("Squares a number.")]
            public async Task SquareAsync([Summary("The number to square.")] int num)
            {
                // We can also access the channel from the Command Context.
                await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
            }

            // ~sample userinfo --> foxbot#0282
            // ~sample userinfo @Khionu --> Khionu#8708
            // ~sample userinfo Khionu#8708 --> Khionu#8708
            // ~sample userinfo Khionu --> Khionu#8708
            // ~sample userinfo 96642168176807936 --> Khionu#8708
            // ~sample whois 96642168176807936 --> Khionu#8708
            [Command("userinfo", RunMode = RunMode.Async)]
            [Summary("Returns info about the current user, or the user parameter, if one passed.")]
            [Alias("user", "whois")]
            public async Task UserInfoAsync([Summary("The (optional) user to get info for")] SocketUser user = null)
            {
                var userInfo = user ?? Context.Client.CurrentUser;
                await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
            }
        }
    }
}
