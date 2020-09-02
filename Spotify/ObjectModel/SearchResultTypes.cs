using System;

namespace Spotify.ObjectModel
{
    [Flags]
    public enum SearchResultTypes : Int32
    {
        Album = 0x01,
        Artist = 0x02,
        Playlist = 0x04,
        Track = 0x08,
        Show = 0x10,
        Episode = 0x20
    }
}