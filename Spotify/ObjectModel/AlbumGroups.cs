using System;

namespace Spotify.ObjectModel
{
    [Flags]
    public enum AlbumGroups : Int32
    {
        Album = 0x01,
        Single = 0x02,
        Compilation = 0x04,
        AppearsOn = 0x08
    }
}