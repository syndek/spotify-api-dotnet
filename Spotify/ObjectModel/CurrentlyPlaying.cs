using System;

namespace Spotify.ObjectModel
{
    public record CurrentlyPlaying : SpotifyObject
    {
        internal CurrentlyPlaying(
            CurrentlyPlayingType type,
            IPlayable? item,
            Boolean playing,
            Int32? progress,
            Context? context,
            Int32 timestamp)
            : base()
        {
            this.Type = type;
            this.Item = item;
            this.IsPlaying = playing;
            this.Progress = progress;
            this.Context = context;
            this.Timestamp = timestamp;
        }

        public CurrentlyPlayingType Type { get; init; }
        public IPlayable? Item { get; init; }
        public Boolean IsPlaying { get; init; }
        public Int32? Progress { get; init; }
        public Context? Context { get; init; }
        public Int32 Timestamp { get; init; }
    }
}