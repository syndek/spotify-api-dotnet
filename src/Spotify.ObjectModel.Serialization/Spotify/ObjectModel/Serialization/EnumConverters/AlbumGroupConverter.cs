using System;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class AlbumGroupConverter
    {
        public static AlbumGroups FromSpotifyString(string albumGroup) => albumGroup switch
        {
            "album" => AlbumGroups.Album,
            "single" => AlbumGroups.Single,
            "compilation" => AlbumGroups.Compilation,
            "appears_on" => AlbumGroups.AppearsOn,
            _ => throw new ArgumentException($"Invalid {nameof(AlbumGroups)} string value: {albumGroup}", nameof(albumGroup))
        };

        public static AlbumGroups FromSpotifyStrings(IEnumerable<string> albumGroups) =>
            albumGroups.Aggregate(
                new AlbumGroups(),
                (current, albumGroup) => current | FromSpotifyString(albumGroup));

        public static string ToSpotifyString(this AlbumGroups albumGroup) => albumGroup switch
        {
            AlbumGroups.Album => "album",
            AlbumGroups.Single => "single",
            AlbumGroups.Compilation => "compilation",
            AlbumGroups.AppearsOn => "appears_on",
            _ => throw new ArgumentException($"Invalid {nameof(AlbumGroups)} value: {albumGroup}", nameof(albumGroup))
        };

        public static IEnumerable<string> ToSpotifyStrings(this AlbumGroups albumGroups) =>
            albumGroups.GetFlags().Select(ToSpotifyString);
    }
}
