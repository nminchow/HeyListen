using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeyListen.Controllers.Helpers
{
    class SpotifyAuth
    {
        public static async System.Threading.Tasks.Task<SpotifyWebAPI> PerformAsync(User user, DataBase db, AdminOrchestrator config, ISocketMessageChannel channel)
        {
            if(string.IsNullOrEmpty(user.SpotifyToken) || string.IsNullOrEmpty(user.RefreshToken))
            {
                var dj = await channel.GetUserAsync(Convert.ToUInt64(user.DiscordId));
                await channel.SendMessageAsync("The currend DJ is not properly configured! They were just direct messaged a new authentication link.");
                await Discord.UserExtensions.SendMessageAsync(dj, text: "", embed: Views.Admin.AuthUrl.Response());
            }

            if(DateTime.Now > user.TokenExpires)
            {
                Token t = new AutorizationCodeAuth() { ClientId = "2c2de92637fb4ebf88074f4e473db04d", RedirectUri = "https://nminchow.github.io/HeyListenWeb/" }.RefreshToken(user.RefreshToken, config.secret);
                user.SpotifyToken = t.AccessToken;
                user.TokenExpires = DateTime.Now.AddSeconds(t.ExpiresIn);
                db.SaveChanges();
            }

            var spotify = new SpotifyWebAPI()
            {
                AccessToken = user.SpotifyToken,
                TokenType = "Bearer"
            };


            return spotify;
        }
    }
}
