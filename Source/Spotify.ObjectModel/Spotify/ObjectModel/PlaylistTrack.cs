using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a track of a <see cref="Playlist"/>.
    /// </summary>
    public record PlaylistTrack : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistTrack"/> record with the specified values.
        /// </summary>
        /// <param name="addedAt">The <see cref="DateTime"/> at which the <paramref name="track"/> was added to the playlist.</param>
        /// <param name="addedBy">The <see cref="PublicUser"/> who added the <paramref name="track"/> to the playlist.</param>
        /// <param name="isLocal">A <see cref="bool"/> indicating whether or not the <paramref name="track"/> is a local file.</param>
        /// <param name="track">A <see cref="IPlayable"/> object representing the track that is in the playlist.</param>
        public PlaylistTrack(DateTime addedAt, PublicUser addedBy, bool isLocal, IPlayable track)
        {
            AddedAt = addedAt;
            AddedBy = addedBy;
            IsLocal = isLocal;
            Track = track;
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> at which the <see cref="Track"/> was added to the playlist.
        /// </summary>
        /// <returns>The <see cref="DateTime"/> at which the <see cref="Track"/> was added to the playlist.</returns>
        public DateTime AddedAt { get; init; }
        /// <summary>
        /// Gets or sets the <see cref="PublicUser"/> who added the <see cref="Track"/> to the playlist.
        /// </summary>
        /// <returns>The <see cref="PublicUser"/> who added the <see cref="Track"/> to the playlist.</returns>
        public PublicUser AddedBy { get; init; }
        /// <summary>
        /// Gets or sets a value indicating whether or not the <see cref="Track"/> is a local file.
        /// </summary>
        /// <returns>A <see cref="bool"/> indicating whether or not the <see cref="Track"/> is a local file.</returns>
        public bool IsLocal { get; init; }
        /// <summary>
        /// Gets or sets the track that is in the playlist.
        /// </summary>
        /// <remarks>Currently, this value can either be a <see cref="ObjectModel.Track"/> or an <see cref="Episode"/>.</remarks>
        /// <returns>A <see cref="IPlayable"/> object representing the track that is in the playlist.</returns>
        public IPlayable Track { get; init; }
    }
}