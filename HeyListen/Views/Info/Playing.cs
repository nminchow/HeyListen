using System.Web;
using Discord;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;

namespace HeyListen.Views.Info
{
    public static class Playing
    {
        public static Embed Response()
        {
            var spotify = new SpotifyWebAPI()
            {
                AccessToken = "",
                TokenType = "Bearer"
            };
            PlaybackContext context = spotify.GetPlayingTrack();

            var embed = new EmbedBuilder();
            embed.Title = context.Item.Name;
            return embed;
        }
    }
}
