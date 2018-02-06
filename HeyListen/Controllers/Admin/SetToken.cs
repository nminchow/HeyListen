
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web.Auth;
using Microsoft.Extensions.Configuration;
using System;

namespace HeyListen.Controllers.Admin
{
    class SetToken
    {
        public static async System.Threading.Tasks.Task PerformAsync(Discord.WebSocket.SocketUser user, string token, DataBase db, AdminOrchestrator config)
        {
            Token t = new AutorizationCodeAuth() { ClientId = "2c2de92637fb4ebf88074f4e473db04d", RedirectUri = "https://nminchow.github.io/HeyListenWeb/getToken.html" }.ExchangeAuthCode(token, config.secret);
            

            var dbUser = Helpers.FindOrCreateUser.Perform(user, db);
            dbUser.SpotifyToken = t.AccessToken;
            dbUser.RefreshToken = t.RefreshToken;
            dbUser.TokenExpires = DateTime.Now.AddSeconds(t.ExpiresIn);
            db.SaveChanges();
            await Discord.UserExtensions.SendMessageAsync(user, text: "You are all set!");
        }
    }
}
