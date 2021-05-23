namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines different types of <see cref="ObjectModel.Album"/>.
    /// </summary>
    public enum AlbumType
    {
        /// <summary>
        /// The <see cref="ObjectModel.Album"/> is a regular album.
        /// </summary>
        Album,
        /// <summary>
        /// The <see cref="ObjectModel.Album"/> is a single.
        /// </summary>
        Single,
        /// <summary>
        /// The <see cref="ObjectModel.Album"/> is a compilation of tracks from other albums.
        /// </summary>
        Compilation
    }
}
