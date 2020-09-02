using System;

namespace Spotify.ObjectModel
{
    public record ResumePoint : SpotifyObject
    {
        internal ResumePoint(Int32 resumePosition, Boolean isFullyPlayed) : base()
        {
            this.ResumePosition = resumePosition;
            this.IsFullyPlayed = isFullyPlayed;
        }

        public Int32 ResumePosition { get; init; }
        public Boolean IsFullyPlayed { get; init; }
    }
}