using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a playlist of tracks or episodes.
    /// </summary>
    public record Playlist : SimplifiedPlaylist
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Playlist"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the playlist.</param>
        /// <param name="uri">The Spotify URI of the playlist.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the playlist.</param>
        /// <param name="name">A <see cref="string"/> representing the name of the playlist.</param>
        /// <param name="description">
        /// A <see cref="string"/> representing the description of the playlist,
        /// or <see langword="null"/> if none is provided.
        /// </param>
        /// <param name="images">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects representing the cover art of the playlist.
        /// </param>
        /// <param name="owner">The <see cref="PublicUser"/> who owns the playlist.</param>
        /// <param name="followers">The <see cref="ObjectModel.Followers"/> of the playlist.</param>
        /// <param name="tracks">
        /// A <see cref="Paging{TItem}"/> of <see cref="PlaylistTrack"/> objects representing the tracks of the playlist.
        /// </param>
        /// <param name="isPublic">
        /// A nullable <see cref="bool"/> representing the visibility of the playlist:
        /// <see langword="true"/> if the playlist is public, <see langword="false"/> if the playlist is private,
        /// or <see langword="null"/> if the visibility is not relevant.
        /// </param>
        /// <param name="isCollaborative">
        /// A <see cref="bool"/> indicating whether or not the <paramref name="owner"/> allows other users to modify the playlist.
        /// </param>
        /// <param name="snapshotId">A <see cref="string"/> representing the version identifier of the current playlist.</param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the playlist, keyed by the type of the URL.
        /// </param>
        public Playlist(
            string id,
            Uri uri,
            Uri href,
            string name,
            string? description,
            IReadOnlyList<Image> images,
            PublicUser owner,
            Followers followers,
            Paging<PlaylistTrack> tracks,
            bool? isPublic,
            bool isCollaborative,
            string snapshotId,
            IReadOnlyDictionary<string, Uri> externalUrls)
            : base(
                id,
                uri,
                href,
                name,
                description,
                images,
                owner,
                tracks,
                isPublic,
                isCollaborative,
                snapshotId,
                externalUrls)
        {
            Followers = followers;
        }

        /// <summary>
        /// Gets or sets the <see cref="ObjectModel.Followers"/> of the <see cref="Playlist"/>.
        /// </summary>
        /// <returns>The <see cref="ObjectModel.Followers"/> of the <see cref="Playlist"/>.</returns>
        public Followers Followers { get; init; }
    }
}