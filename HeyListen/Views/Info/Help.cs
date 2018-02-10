using Discord;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Views.Info
{
    public static class Help
    {
        public static Tuple<string, Embed> Response()
        {

            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = "Guide"
                },
                ThumbnailUrl = "https://nminchow.github.io/HeyListenWeb/images/demo/heylogo.png",
                Description = "'HeyListen!' is a bot that assists with discord's integrated spotify sharing.\n\n" +
                "Prepend all commands with '!hey' - ex. ```!hey help```\n" +
                "'HeyListen!' revolves around the concept of a `DJ`. Commands in the channel will control the spotify account of the DJ. In most cases, " +
                "you will want this to be the person who is doing the spotify sharing. This will allow other users in the channel to control " +
                "the DJ's playback (and thus what everyone is listening to) if the DJ allows it.\n\n" +
                "The DJ can also create a playlist to which other users in the channel can contribute songs. Its important to note " +
                "that this is a spotify playlist, and if the DJ stops playing from that playlist, the other user's additions will no longer be " +
                "played.\n\n" +
                "**Commands:**",
                Color = new Color(150, 230, 253)

            };

            embed.AddField("dj {user name}", "Sets the current dj to the supplied user." +
                "\n`!hey dj navi`" +
                "\nIf no user is supplied, sets the DJ to the caller. Requires the caller be a channel modderator.");
            embed.AddField("clear", "If you are the DJ, removes all channel control from your spotify playback.");
            embed.AddField("playlist", "If you are the DJ, creates a playlist for you. Other players can then add tracks to this list.");
            embed.AddField("skip", "Skips the currently playing tack.");
            embed.AddField("back", "Skips back a track.");
            embed.AddField("add {song name}", "If the current user has generated a playlist, adds a track to it. If multiple tracks are found, displays search results." +
                "\n`!hey add Lost Woods`");
            embed.AddField("allow playback {true|false}", "Enables/disables non-dj playback skipping.");
            embed.AddField("allow playlist {true|false}", "Enables/disables non-dj playlist additions.");

            return new Tuple<string, Embed>(GetMessage(), embed);
        }

        private static string GetMessage()
        {
            return "";
        }
    }
}
