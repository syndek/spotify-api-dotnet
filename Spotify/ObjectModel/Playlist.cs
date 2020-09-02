using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Playlist : SimplifiedPlaylist
    {
        internal Playlist(
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

        public Followers Followers { get; init; }
    }
}