using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Discord;


namespace HeyListen.Views.Admin
{
    public static class AuthUrl
    {
        public static Embed Response()
        {
            var embed = new EmbedBuilder();
            embed.Title = "It looks like you have requested or been assigned DJ access. To get rolling, please authroize 'HeyListen!' using the link below.";
            var permissions = HttpUtility.UrlPathEncode("playlist-modify-public user-read-playback-state user-modify-playback-state");
            var url = HttpUtility.UrlPathEncode("https://nminchow.github.io/HeyListenWeb/getToken.html");
            embed.Description = $"[authorize HeyListen!](https://accounts.spotify.com/authorize?client_id=2c2de92637fb4ebf88074f4e473db04d&response_type=code&scope={permissions}&redirect_uri={url})";
            return embed;
        }
    }
}
