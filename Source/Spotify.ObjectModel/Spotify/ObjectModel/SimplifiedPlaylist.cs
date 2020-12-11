using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a simplified view of a <see cref="Playlist"/>.
    /// </summary>
    public record SimplifiedPlaylist : LocatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedPlaylist"/> record with the specified values.
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
        public SimplifiedPlaylist(
            string id,
            Uri uri,
            Uri href,
            string name,
            string? description,
            IEnumerable<Image> images,
            PublicUser owner,
            Paging<PlaylistTrack> tracks,
            bool? isPublic,
            bool isCollaborative,
            string snapshotId,
            IEnumerable<KeyValuePair<string, Uri>> externalUrls)
            : base(id, uri)
        {
            Href = href;
            Name = name;
            Description = description;
            Images = new ImmutableValueArray<Image>(images);
            Owner = owner;
            Tracks = tracks;
            IsPublic = isPublic;
            IsCollaborative = isCollaborative;
            SnapshotId = snapshotId;
            ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint providing full details of the <see cref="SimplifiedPlaylist"/>.</returns>
        public Uri Href { get; init; }

        /// <summary>
        /// Gets or sets the name of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the name of the <see cref="SimplifiedPlaylist"/>.</returns>
        public string Name { get; init; }

        /// <summary>
        /// Gets or sets the description of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <remarks>Only present in modified, verified playlists, otherwise <see langword="null"/>.</remarks>
        /// <returns>
        /// A <see cref="string"/> representing the description of the <see cref="SimplifiedPlaylist"/>,
        /// or <see langword="null"/> if none is provided.
        /// </returns>
        public string? Description { get; init; }

        /// <summary>
        /// Gets or sets the cover art of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing the cover art of the <see cref="SimplifiedPlaylist"/> in various sizes.
        /// </returns>
        public IReadOnlyList<Image> Images { get; init; }

        /// <summary>
        /// Gets or sets the owner of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>The <see cref="PublicUser"/> who owns the <see cref="SimplifiedPlaylist"/>.</returns>
        public PublicUser Owner { get; init; }

        /// <summary>
        /// Gets or sets the tracks of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of <see cref="PlaylistTrack"/> objects
        /// representing the tracks of the <see cref="SimplifiedPlaylist"/>.
        /// </returns>
        public Paging<PlaylistTrack> Tracks { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the <see cref="SimplifiedPlaylist"/> is public.
        /// </summary>
        /// <returns>
        /// A nullable <see cref="bool"/> representing the visibility of the playlist:
        /// <see langword="true"/> if the playlist is public, <see langword="false"/> if the playlist is private,
        /// or <see langword="null"/> if the visibility is not relevant.
        /// </returns>
        public bool? IsPublic { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the <see cref="SimplifiedPlaylist"/> is collaborative.
        /// </summary>
        /// <returns>
        /// A <see cref="bool"/> indicating whether or not the <see cref="Owner"/>
        /// allows other users to modify the <see cref="SimplifiedPlaylist"/>.
        /// </returns>
        public bool IsCollaborative { get; init; }

        /// <summary>
        /// Gets or sets the version identifier of the current <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returnsA <see cref="string"/> representing the version identifier of the current <see cref="SimplifiedPlaylist"/>.</returns>
        public string SnapshotId { get; init; }

        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the <see cref="SimplifiedPlaylist"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
    }
}