using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a generic object used to represent various time intervals within an <see cref="AudioAnalysis"/> object.
    /// </summary>
    public record TimeInterval : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeInterval"/> record with the
        /// specified <paramref name="start"/>, <paramref name="duration"/>, and <paramref name="confidence"/>.
        /// </summary>
        /// <param name="start">A <see cref="Single"/> representing the starting point (in seconds) of the time interval.</param>
        /// <param name="duration">A <see cref="Single"/> representing the duration (in seconds) of the time interval.</param>
        /// <param name="confidence">A <see cref="Single"/> representing the confidence of the reliability of the time interval.</param>
        public TimeInterval(Single start, Single duration, Single confidence) : base()
        {
            this.Start = start;
            this.Duration = duration;
            this.Confidence = confidence;
        }

        /// <summary>
        /// Gets or sets the starting point of the <see cref="TimeInterval"/>.
        /// </summary>
        /// <returns>A <see cref="Single"/> representing the starting point (in seconds) of the <see cref="TimeInterval"/>.</returns>
        public Single Start { get; init; }
        /// <summary>
        /// Gets or sets the duration of the <see cref="TimeInterval"/>.
        /// </summary>
        /// <returns>A <see cref="Single"/> representing the duration (in seconds) of the <see cref="TimeInterval"/>.</returns>
        public Single Duration { get; init; }
        /// <summary>
        /// Gets or sets the confidence, from <c>0.0</c> to <c>1.0</c>, of the <see cref="TimeInterval"/>.
        /// </summary>
        /// <returns>A <see cref="Single"/> representing the confidence of the reliability of the <see cref="TimeInterval"/>.</returns>
        public Single Confidence { get; init; }
    }
}