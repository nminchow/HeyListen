using Discord.WebSocket;
using HeyListen.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyListen.Controllers.Playlist
{
    class AddUri
    {
        public static async Task PerformAsync(ISocketMessageChannel channel, SpotifyWebAPI spotify, User dj, string uri)
        {
            var userId = spotify.GetPrivateProfile().Id;

            spotify.AddPlaylistTrack(userId, dj.Playlist, uri);
            var track = spotify.GetTrack(uri.Split(':').Last());

            var current = spotify.GetPlayback();
            //if (current.Context.Uri.Split(':').Last() != dj.Playlist)
            //    spotify.ResumePlayback(contextUri: dj.Playlist);

            if (!track.HasError())
                await channel.SendMessageAsync(text: $"Track Added: {track.Name}");
        }
    }
}
