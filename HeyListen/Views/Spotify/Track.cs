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
                    Name = track.Name
                },
                ThumbnailUrl = track.Album.Images[0].Url
            };
            embed.AddField("Artist", track.Artists[0]?.Name);
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
    }
}
