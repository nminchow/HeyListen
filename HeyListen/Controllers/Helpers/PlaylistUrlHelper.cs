using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Controllers.Helpers
{
    class PlaylistUrlHelper
    {
        public static string GenerateUrl(string playlistUri)
        {
            var split = playlistUri.Split(':');
            var url = $"https://open.spotify.com/user/{split[2]}/playlist/{split[4]}";
            return $"This channel can now add songs to the following playlist: {url}";
        }
    }
}
