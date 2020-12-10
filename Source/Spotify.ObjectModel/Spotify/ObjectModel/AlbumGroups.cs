using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines different relationships between an <see cref="Artist"/> and an <see cref="ObjectModel.Album"/>.
    /// </summary>
    [Flags]
    public enum AlbumGroups
    {
        /// <summary>
        /// The <see cref="ObjectModel.Album"/> is an album by the <see cref="Artist"/>.
        /// </summary>
        Album = 0x01,
        /// <summary>
        /// The <see cref="ObjectModel.Album"/> is a single by the <see cref="Artist"/>.
        /// </summary>
        Single = 0x02,
        /// <summary>
        /// The <see cref="ObjectModel.Album"/> is a compilation by the <see cref="Artist"/>.
        /// </summary>
        Compilation = 0x04,
        /// <summary>
        /// The <see cref="Artist"/> appears on the <see cref="ObjectModel.Album"/>, but it is not by them.
        /// </summary>
        AppearsOn = 0x08
    }
}