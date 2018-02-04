using Discord.WebSocket;
using SpotifyAPI.Web;


namespace HeyListen.Controllers.Playback
{
    class Play
    {
        public static async System.Threading.Tasks.Task PerformAsync(ISocketMessageChannel channel, SpotifyWebAPI spotify)
        {
            spotify.ResumePlayback();
            await channel.SendMessageAsync(text: $"music resumed");
        }
    }
}
