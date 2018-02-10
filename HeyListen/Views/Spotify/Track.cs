using Discord;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Views.Spotify
{
    public static class Track
    {
        public static Embed Response(FullTrack track, bool includeUir = false)
        {
            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = track.Name,
                    Url = track.ExternUrls["spotify"],
                    IconUrl = "https://nminchow.github.io/HeyListenWeb/images/demo/Spotify_Icon_RGB_Green.png"
                },
                ThumbnailUrl = track.Album.Images[0].Url,
                Color = new Color(30, 215, 96)
            };
            embed.AddInlineField("Artist", $"[{truncateName(track.Artists[0]?.Name)}]({track.Artists[0]?.ExternalUrls["spotify"]})");
            embed.AddInlineField("Album", $"[{truncateName(track.Album?.Name)}]({track.Album?.ExternalUrls["spotify"]})");
            embed.AddInlineField("Popularity", track.Popularity);
            TimeSpan t = TimeSpan.FromMilliseconds(track.DurationMs);
            embed.AddInlineField("Duration", $"{t.Minutes}:{t.Seconds.ToString().PadLeft(2, '0')}");
            if (includeUir)
            {
                embed.Footer = new EmbedFooterBuilder
                {
                    Text = $"!hey add_uri {track.Uri}"
                };
            }
            return embed;
        }

        private static string truncateName(string name)
        {
            var length = 20;

            if (string.IsNullOrEmpty(name) || name.Length <= length)
                return name ;
            return $"{name.Substring(0, length - 4).Trim()}...";
        }
    }
}
