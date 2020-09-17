using System;

namespace Spotify.ObjectModel
{
    public record Saved<TSaved> : SpotifyObject where TSaved : ISaveable
    {
        public Saved(TSaved savedObject, DateTime savedAt) : base()
        {
            this.SavedObject = savedObject;
            this.SavedAt = savedAt;
        }

        public TSaved SavedObject { get; init; }
        public DateTime SavedAt { get; init; }
    }
}