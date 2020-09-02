using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class AlbumTypeConverter : Object
    {
        internal static AlbumType FromSpotifyString(String albumType) => albumType.ToLower() switch
        {
            "album" => AlbumType.Album,
            "single" => AlbumType.Single,
            "compilation" => AlbumType.Compilation,
            _ => throw new ArgumentException($"Invalid {nameof(AlbumType)} string value: {albumType}", nameof(albumType))
        };

        internal static String ToSpotifyString(this AlbumType albumType) => albumType switch
        {
            AlbumType.Album => "album",
            AlbumType.Single => "single",
            AlbumType.Compilation => "compilation",
            _ => throw new ArgumentException($"Invalid {nameof(AlbumType)} value: {albumType}", nameof(albumType))
        };
    }
}