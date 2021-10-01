﻿using System.Collections.Generic;
using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a low-level audio analysis for a <see cref="Track"/> in the Spotify catalog.
    /// </summary>
    public record AudioAnalysis : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioAnalysis"/> record with the specified values.
        /// </summary>
        /// <param name="bars">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="TimeInterval"/> objects
        /// representing the time intervals of bars throughout the <see cref="Track"/>.
        /// </param>
        /// <param name="beats">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="TimeInterval"/> objects
        /// representing the time intervals of beats throughout the <see cref="Track"/>.
        /// </param>
        /// <param name="sections">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Section"/> objects
        /// representing different sections of the <see cref="Track"/>.
        /// </param>
        /// <param name="segments">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Segment"/> objects
        /// representing different segments of the <see cref="Track"/>.
        /// </param>
        /// <param name="tatums">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="TimeInterval"/> objects
        /// representing tatums present throughout the <see cref="Track"/>.
        /// </param>
        public AudioAnalysis(
            IEnumerable<TimeInterval> bars,
            IEnumerable<TimeInterval> beats,
            IEnumerable<Section> sections,
            IEnumerable<Segment> segments,
            IEnumerable<TimeInterval> tatums)
        {
            Bars = new ImmutableValueArray<TimeInterval>(bars);
            Beats = new ImmutableValueArray<TimeInterval>(beats);
            Sections = new ImmutableValueArray<Section>(sections);
            Segments = new ImmutableValueArray<Segment>(segments);
            Tatums = new ImmutableValueArray<TimeInterval>(tatums);
        }

        /// <summary>
        /// Gets or sets the bars of the <see cref="AudioAnalysis"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="TimeInterval"/> objects
        /// representing the time intervals of bars throughout the <see cref="Track"/>.
        /// </returns>
        public IReadOnlyList<TimeInterval> Bars { get; init; }

        /// <summary>
        /// Gets or sets the beats of the <see cref="AudioAnalysis"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="TimeInterval"/> objects
        /// representing the time intervals of beats throughout the <see cref="Track"/>.
        /// </returns>
        public IReadOnlyList<TimeInterval> Beats { get; init; }

        /// <summary>
        /// Gets or sets the sections of the <see cref="AudioAnalysis"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Section"/> objects
        /// representing different sections of the <see cref="Track"/>.
        /// </returns>
        public IReadOnlyList<Section> Sections { get; init; }

        /// <summary>
        /// Gets or sets the segments of the <see cref="AudioAnalysis"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Section"/> objects
        /// representing different segments of the <see cref="Track"/>.
        /// </returns>
        public IReadOnlyList<Segment> Segments { get; init; }

        /// <summary>
        /// Gets or sets the tatums of the <see cref="AudioAnalysis"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="TimeInterval"/> objects
        /// representing tatums present throughout the <see cref="Track"/>.
        /// </returns>
        public IReadOnlyList<TimeInterval> Tatums { get; init; }
    }
}
