using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a simplified view of a <see cref="Playlist"/>.
    /// </summary>
    public record SimplifiedPlaylist : LocatableObject
    {
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
            this.Images = images;
            this.Owner = owner;
            this.Tracks = tracks;
            this.IsPublic = isPublic;
            this.IsCollaborative = isCollaborative;
            this.SnapshotId = snapshotId;
            this.ExternalUrls = externalUrls;
        }

        public Uri Href { get; init; }
        public String Name { get; init; }
        public String? Description { get; init; }
        public IReadOnlyList<Image> Images { get; init; }
        public PublicUser Owner { get; init; }
        public Paging<PlaylistTrack> Tracks { get; init; }
        public Boolean? IsPublic { get; init; }
        public Boolean IsCollaborative { get; init; }
        public String SnapshotId { get; init; }
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}