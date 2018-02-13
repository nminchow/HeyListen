using System.Linq;
using HeyListen.Models;
using Discord.WebSocket;
using HeyListen.Controllers.Helpers;
using System;

namespace HeyListen.Controllers.Admin
{
    static class SwithToNewPlaylist
    {
        public static async System.Threading.Tasks.Task PerformAsync(SocketUser user, ISocketMessageChannel channel, DataBase db, AdminOrchestrator config)
        {
            var dbUser = FindOrCreateUser.Perform(user, db);

            var spotify = await SpotifyAuth.PerformAsync(dbUser, db, config, channel);
             
            var userId = spotify.GetPrivateProfile().Id;

            var playlist = spotify.CreatePlaylist(userId, "HeyListen!", true);
            if (playlist.HasError())
            {
                return;
            }
            dbUser.Playlist = playlist.Id;
            db.SaveChanges();
            spotify.AddPlaylistTrack(userId, playlist.Id, "spotify:track:0ldPfYO58u8zo9Mmj03z8n");

            //spotify.ResumePlayback(contextUri: playlist.Uri);
            spotify.SetRepeatMode(SpotifyAPI.Web.Enums.RepeatState.Off);

            await channel.SendMessageAsync(text: $"Playlist created! This channel can now add songs to the following playlist: {playlist.ExternalUrls["spotify"]}");
        }
    }
}
