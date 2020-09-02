using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class AlbumGroupConverter : Object
    {
        internal static AlbumGroup FromSpotifyString(String albumGroup) => albumGroup.ToLower() switch
        {
            "album" => AlbumGroup.Album,
            "single" => AlbumGroup.Single,
            "compilation" => AlbumGroup.Compilation,
            "appears_on" => AlbumGroup.AppearsOn,
            _ => throw new ArgumentException($"Invalid {nameof(AlbumGroup)} string value: {albumGroup}", nameof(albumGroup))
        };

        internal static String ToSpotifyString(this AlbumGroup albumGroup) => albumGroup switch
        {
            AlbumGroup.Album => "album",
            AlbumGroup.Single => "single",
            AlbumGroup.Compilation => "compilation",
            AlbumGroup.AppearsOn => "appears_on",
            _ => throw new ArgumentException($"Invalid {nameof(AlbumGroup)} value: {albumGroup}", nameof(albumGroup))
        };
    }
}