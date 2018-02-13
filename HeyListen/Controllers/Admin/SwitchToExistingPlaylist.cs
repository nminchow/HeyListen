using System.Linq;
using HeyListen.Models;
using Discord.WebSocket;
using HeyListen.Controllers.Helpers;
using System;

namespace HeyListen.Controllers.Admin
{
    static class SwithToExistingPlaylist
    {
        public static async System.Threading.Tasks.Task PerformAsync(SocketUser user, ISocketMessageChannel channel, DataBase db, AdminOrchestrator config, string searchTerm)
        {
            var dbUser = FindOrCreateUser.Perform(user, db);

            var spotify = await SpotifyAuth.PerformAsync(dbUser, db, config, channel);
             
            var userId = spotify.GetPrivateProfile().Id;

            var playlists = await spotify.GetUserPlaylistsAsync(userId, 100, 0);

            if (playlists.HasError())
            {
                return;
            }

            var playlist = playlists.Items.FirstOrDefault(p => p.Name.ToLower().Contains(searchTerm));

            if (playlist == null)
            {
                await channel.SendMessageAsync(text: "No playlist found");
                return;
            }
            
            dbUser.Playlist = playlist.Id;
            db.SaveChanges();

            spotify.SetRepeatMode(SpotifyAPI.Web.Enums.RepeatState.Off);

            await channel.SendMessageAsync(text: $"This channel can now add songs to the following playlist: {playlist.ExternalUrls["spotify"]}");
        }
    }
}
