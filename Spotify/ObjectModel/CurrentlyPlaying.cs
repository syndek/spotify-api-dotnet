using System;

namespace Spotify.ObjectModel
{
    public sealed class CurrentlyPlaying : Object
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

        public CurrentlyPlayingType Type { get; }
        public IPlayable? Item { get; }
        public Boolean IsPlaying { get; }
        public Int32? Progress { get; }
        public Context? Context { get; }
        public Int32 Timestamp { get; }
    }
}