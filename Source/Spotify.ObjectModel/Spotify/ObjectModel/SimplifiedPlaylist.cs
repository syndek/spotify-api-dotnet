using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

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
        public SimplifiedPlaylist(
            String id,
            Uri uri,
            Uri href,
            String name,
            String? description,
            IReadOnlyList<Image> images,
            PublicUser owner,
            Paging<PlaylistTrack> tracks,
            Boolean? isPublic,
            Boolean isCollaborative,
            String snapshotId,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.Description = description;
            this.Images = new ImmutableValueArray<Image>(images);
            this.Owner = owner;
            this.Tracks = tracks;
            this.IsPublic = isPublic;
            this.IsCollaborative = isCollaborative;
            this.SnapshotId = snapshotId;
            this.ExternalUrls = new ImmutableValueDictionary<String, Uri>(externalUrls);
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint providing full details of the <see cref="SimplifiedPlaylist"/>.</returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the name of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the name of the <see cref="SimplifiedPlaylist"/>.</returns>
        public String Name { get; init; }
        /// <summary>
        /// Gets or sets the description of the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <remarks>Only present in modified, verified playlists, otherwise <see langword="null"/>.</remarks>
        /// <returns>
        /// A <see cref="String"/> representing the description of the <see cref="SimplifiedPlaylist"/>,
        /// or <see langword="null"/> if none is provided.
        /// </returns>
        public String? Description { get; init; }
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
        /// A nullable <see cref="Boolean"/> representing the visibility of the playlist:
        /// <see langword="true"/> if the playlist is public, <see langword="false"/> if the playlist is private,
        /// or <see langword="null"/> if the visibility is not relevant.
        /// </returns>
        public Boolean? IsPublic { get; init; }
        /// <summary>
        /// Gets or sets a value indicating whether or not the <see cref="SimplifiedPlaylist"/> is collaborative.
        /// </summary>
        /// <returns>
        /// A <see cref="Boolean"/> indicating whether or not the <see cref="Owner"/>
        /// allows other users to modify the <see cref="SimplifiedPlaylist"/>.
        /// </returns>
        public Boolean IsCollaborative { get; init; }
        /// <summary>
        /// Gets or sets the version identifier of the current <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returnsA <see cref="String"/> representing the version identifier of the current <see cref="SimplifiedPlaylist"/>.</returns>
        public String SnapshotId { get; init; }
        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="SimplifiedPlaylist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the <see cref="SimplifiedPlaylist"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}