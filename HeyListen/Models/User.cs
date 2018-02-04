using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Models
{
    public class User
    {
        public int ID { get; set; }
        public string DiscordId { get; set; }
        public string SpotifyToken { get; set; }
        public string RefreshToken { get; set; }
        public string Playlist { get; set; }
        public DateTime TokenExpires { get; set; }
        public bool AllowPlaybackControl { get; set; }
        public bool AllowPlaylistControl { get; set; }
    }
}
