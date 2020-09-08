using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents information about a recent play of a <see cref="ObjectModel.Track"/>.
    /// </summary>
    public record PlayHistory : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayHistory"/> record with the specified values.
        /// </summary>
        /// <param name="track">A <see cref="SimplifiedTrack"/> representing the track the user listened to.</param>
        /// <param name="context">The <see cref="ObjectModel.Context"/> the track was played from.</param>
        /// <param name="playedAt">The date and time the track was played.</param>
        internal PlayHistory(SimplifiedTrack track, Context context, DateTime playedAt) : base()
        {
            this.Track = track;
            this.Context = context;
            this.PlayedAt = playedAt;
        }

        /// <summary>
        /// Gets or sets the track the user listened to.
        /// </summary>
        /// <returns>A <see cref="SimplifiedTrack"> representing the track the user listened to.</returns>
        public SimplifiedTrack Track { get; init; }
        /// <summary>
        /// Gets or sets the <see cref="ObjectModel.Context"/> the track was played from.
        /// </summary>
        /// <returns>The <see cref="ObjectModel.Context"/> the track was played from.</returns>
        public Context Context { get; init; }
        /// <summary>
        /// Gets or sets the date and time the track was played.
        /// </summary>
        /// <returns>The date and time the track was played.</returns>
        public DateTime PlayedAt { get; init; }
    }
}