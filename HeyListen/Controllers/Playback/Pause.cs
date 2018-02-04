using Discord.WebSocket;
using SpotifyAPI.Web;


namespace HeyListen.Controllers.Playback
{
    class Pause
    {
        public static async System.Threading.Tasks.Task PerformAsync(ISocketMessageChannel channel, SpotifyWebAPI spotify)
        {
            var result = spotify.PausePlayback();
            await channel.SendMessageAsync(text: $"music paused");
        }
    }
}
