using System;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class AlbumTypeConverter
    {
        public static AlbumType FromSpotifyString(String albumType) => albumType switch
        {
            "album" => AlbumType.Album,
            "single" => AlbumType.Single,
            "compilation" => AlbumType.Compilation,
            _ => throw new ArgumentException($"Invalid {nameof(AlbumType)} string value: {albumType}", nameof(albumType))
        };

        public static String ToSpotifyString(this AlbumType albumType) => albumType switch
        {
            AlbumType.Album => "album",
            AlbumType.Single => "single",
            AlbumType.Compilation => "compilation",
            _ => throw new ArgumentException($"Invalid {nameof(AlbumType)} value: {albumType}", nameof(albumType))
        };
    }
}