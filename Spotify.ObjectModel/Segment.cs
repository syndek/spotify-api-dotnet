using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a segment of an <see cref="AudioAnalysis"/> object.
    /// A segment is a subdivision of a <see cref="Track"/> that contains a roughly consistent sound throughout its duration.
    /// </summary>
    public record Segment : TimeInterval
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Segment"/> record with the specified values.
        /// </summary>
        /// <param name="start">A <see cref="Single"/> representing the starting point (in seconds) of the segment.</param>
        /// <param name="duration">A <see cref="Single"/> representing the duration (in seconds) of the segment.</param>
        /// <param name="confidence">A <see cref="Single"/> representing the confidence of the reliability of the segmentation.</param>
        /// <param name="loudnessStart">A <see cref="Single"/> representing the onset loudness (in decibels) of the segment.</param>
        /// <param name="loudnessEnd">A <see cref="Single"/> representing the offset loudness (in decibels) of the segment.</param>
        /// <param name="loudnessMax">A <see cref="Single"/> representing the peak loudness (in decibels) of the segment.</param>
        /// <param name="loudnessMaxTime">
        /// A <see cref="Single"/> representing the segment-relative offset of the <paramref name="loudnessMax"/> value (in seconds).
        /// </param>
        /// <param name="pitches">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Single"/> values representing the pitch content of the segment.
        /// </param>
        /// <param name="timbre">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Single"/> values representing the timbre of the segment.
        /// </param>
        public Segment(
            Single start,
            Single duration,
            Single confidence,
            Single loudnessStart,
            Single loudnessEnd,
            Single loudnessMax,
            Single loudnessMaxTime,
            IReadOnlyList<Single> pitches,
            IReadOnlyList<Single> timbre) :
            base(start, duration, confidence)
        {
            this.LoudnessStart = loudnessStart;
            this.LoudnessEnd = loudnessEnd;
            this.LoudnessMax = loudnessMax;
            this.LoudnessMaxTime = loudnessMaxTime;
            this.Pitches = pitches;
            this.Timbre = timbre;
        }

        /// <summary>
        /// Gets or sets the onset loudness (in decibels) of the <see cref="Segment"/>.
        /// </summary>
        /// <remarks>
        /// Combined with <see cref="LoudnessMax"/> and <see cref="LoudnessMaxTime"/>,
        /// these components can be used to describe the 'attack' of the <see cref="Segment"/>.
        /// </remarks>
        /// <returns>A <see cref="Single"/> representing the onset loudness (in decibels) of the <see cref="Segment"/>.</returns>
        public Single LoudnessStart { get; init; }
        /// <summary>
        /// Gets or sets the offset loudness (in decibels) of the <see cref="Segment"/>.
        /// </summary>
        /// <remarks>
        /// This value should be equivalent to the <see cref="LoudnessStart"/> value of the following <see cref="Segment"/>.
        /// </remarks>
        /// <returns>A <see cref="Single"/> representing the offset loudness (in decibels) of the segment.</returns>
        public Single LoudnessEnd { get; init; }
        /// <summary>
        /// Gets or sets the peak loudness (in decibels) of the <see cref="Segment"/>.
        /// </summary>
        /// <remarks>
        /// Combined with <see cref="LoudnessStart"/> and <see cref="LoudnessMaxTime"/>,
        /// these components can be used to describe the 'attack' of the <see cref="Segment"/>.
        /// </remarks>
        /// <returns>A <see cref="Single"/> representing the peak loudness (in decibels) of the segment.</returns>
        public Single LoudnessMax { get; init; }
        /// <summary>
        /// Gets or sets the segment-relative offset of the <see cref="LoudnessMax"/> value (in seconds).
        /// </summary>
        /// <remarks>
        /// Combined with <see cref="LoudnessStart"/> and <see cref="LoudnessMax"/>,
        /// these components can be used to describe the 'attack' of the <see cref="Segment"/>.
        /// </remarks>
        /// <returns>
        /// A <see cref="Single"/> representing the segment-relative offset of the <see cref="LoudnessMax"/> value (in seconds).
        /// </returns>
        public Single LoudnessMaxTime { get; init; }
        /// <summary>
        /// Gets or sets the pitch values of the <see cref="Segment"/>.
        /// </summary>
        /// <remarks>
        /// The pitch values take the form of a 'chroma' vector corresponding to the 12 pitch classes C, C#, D to B,
        /// with values ranging from <c>0.0</c> to <c>1.0</c> that describe the relative dominance of every pitch in the chromatic scale.
        /// More details about how to interpret the vector can be found on the
        /// <see href="https://spotify.dev/documentation/web-api/reference/tracks/get-audio-analysis#pitch">Spotify for Developers</see> website.
        /// </remarks>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Single"/> values representing the pitch content of the <see cref="Segment"/>.
        /// </returns>
        public IReadOnlyList<Single> Pitches { get; init; }
        /// <summary>
        /// Gets or sets the timbre of the <see cref="Segment"/>.
        /// </summary>
        /// <remarks>
        /// Timbre is the quality of a musical note or sound that distinguishes different types of musical instruments, or voices.
        /// The timbre value takes the form of a timbre vector, which is best used in comparison with other timbre vectors.
        /// More details about how to interpret the vector can be found on the
        /// <see href="https://spotify.dev/documentation/web-api/reference/tracks/get-audio-analysis#timbre">Spotify for Developers</see> website.
        /// </remarks>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Single"/> values representing the timbre of the <see cref="Segment"/>.
        /// </returns>
        public IReadOnlyList<Single> Timbre { get; init; }
    }
}