using System;

namespace Spotify.ObjectModel
{
    public record PlaylistTrack : SpotifyObject
    {
        internal PlaylistTrack(DateTime addedAt, PublicUser addedBy, Boolean isLocal, Object track) : base()
        {
            this.AddedAt = addedAt;
            this.AddedBy = addedBy;
            this.IsLocal = isLocal;
            this.Track = track;
        }

        public DateTime AddedAt { get; init; }
        public PublicUser AddedBy { get; init; }
        public Boolean IsLocal { get; init; }
        public Object Track { get; init; }
    }
}