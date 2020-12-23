using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines types that should be included in the result of a call to <see cref="ISpotifySearchApi.SearchAsync"/>.
    /// </summary>
    [Flags]
    public enum SearchResultTypes
    {
        /// <summary>
        /// Albums should be included in the <see cref="SearchResult"/>.
        /// </summary>
        Album = 0x01,
        /// <summary>
        /// Artists should be included in the <see cref="SearchResult"/>.
        /// </summary>
        Artist = 0x02,
        /// <summary>
        /// Playlists should be included in the <see cref="SearchResult"/>.
        /// </summary>
        Playlist = 0x04,
        /// <summary>
        /// Tracks should be included in the <see cref="SearchResult"/>.
        /// </summary>
        Track = 0x08,
        /// <summary>
        /// Shows should be included in the <see cref="SearchResult"/>.
        /// </summary>
        Show = 0x10,
        /// <summary>
        /// Episodes should be included in the <see cref="SearchResult"/>.
        /// </summary>
        Episode = 0x20
    }
}
