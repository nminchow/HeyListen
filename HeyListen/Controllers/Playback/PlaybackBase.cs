using Discord.WebSocket;
using HeyListen.Controllers.Admin;
using HeyListen.Controllers.Helpers;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Web;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HeyListen.Controllers.Playback
{
    public class PlaybackBase
    {
        public static async Task PerformAsync(Func<ISocketMessageChannel, SpotifyWebAPI, Task> task, ISocketMessageChannel channel, DataBase db, AdminOrchestrator config)
        {
            var dbChannel = db.Channels.Include(c => c.CurrentDj).FirstOrDefault(c => c.DiscordId == channel.Id.ToString());

            var spotify = await SpotifyAuth.PerformAsync(dbChannel.CurrentDj, db, config, channel);

            await task(channel, spotify);
        }

        public static async Task MessageBeforeAfter(ISocketMessageChannel channel, SpotifyWebAPI spotify, Func<ISocketMessageChannel, SpotifyWebAPI, bool> action)
        {
            var playing = spotify.GetPlayingTrack();
            if (playing.HasError() || string.IsNullOrEmpty(playing.Item?.Name))
            {
                await channel.SendMessageAsync(Views.Error.SkipError.Response());
                return;
            }
            await channel.SendMessageAsync("Skipping:", embed: Views.Spotify.Track.Response(playing.Item));
            var hasError = action(channel, spotify);
            if (hasError)
            {
                await channel.SendMessageAsync("Error skipping track. This is most likely caused by being at the beginning/end of a playlist");
                return;
            }
            Thread.Sleep(100);
            var newTrack = spotify.GetPlayingTrack();
            if (newTrack.HasError() || string.IsNullOrEmpty(newTrack.Item?.Name))
            {
                await channel.SendMessageAsync(Views.Error.SkipError.Response());
                return;
            }
            await channel.SendMessageAsync("Now Playing:", embed: Views.Spotify.Track.Response(newTrack.Item));
        }
    }
}
