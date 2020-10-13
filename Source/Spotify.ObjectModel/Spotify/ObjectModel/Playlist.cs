using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

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
        /// <param name="id">A <see cref="String"/> representing the Spotify ID of the playlist.</param>
        /// <param name="uri">The Spotify URI of the playlist.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the playlist.</param>
        /// <param name="name">A <see cref="String"/> representing the name of the playlist.</param>
        /// <param name="description">
        /// A <see cref="String"/> representing the description of the playlist,
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
        /// A nullable <see cref="Boolean"/> representing the visibility of the playlist:
        /// <see langword="true"/> if the playlist is public, <see langword="false"/> if the playlist is private,
        /// or <see langword="null"/> if the visibility is not relevant.
        /// </param>
        /// <param name="isCollaborative">
        /// A <see cref="Boolean"/> indicating whether or not the <paramref name="owner"/> allows other users to modify the playlist.
        /// </param>
        /// <param name="snapshotId">A <see cref="String"/> representing the version identifier of the current playlist.</param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the playlist, keyed by the type of the URL.
        /// </param>
        public Playlist(
            String id,
            Uri uri,
            Uri href,
            String name,
            String? description,
            IReadOnlyList<Image> images,
            PublicUser owner,
            Followers followers,
            Paging<PlaylistTrack> tracks,
            Boolean? isPublic,
            Boolean isCollaborative,
            String snapshotId,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(
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
            this.Followers = followers;
        }

        /// <summary>
        /// Gets or sets the <see cref="ObjectModel.Followers"/> of the <see cref="Playlist"/>.
        /// </summary>
        /// <returns>The <see cref="ObjectModel.Followers"/> of the <see cref="Playlist"/>.</returns>
        public Followers Followers { get; init; }
    }
}