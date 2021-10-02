using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a set of recommended tracks.
    /// </summary>
    public record Recommendations : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recommendations"/> record
        /// with the specified <paramref name="seeds"/> and <paramref name="tracks"/>.
        /// </summary>
        /// <param name="seeds">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="RecommendationSeed"/> objects
        /// representing the seeds of the recommendations.
        /// </param>
        /// <param name="tracks">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="SimplifiedTrack"/> objects
        /// representing the recommended tracks of the recommendations.
        /// </param>
        public Recommendations(IEnumerable<RecommendationSeed> seeds, IEnumerable<SimplifiedTrack> tracks)
        {
            Seeds = new ImmutableValueArray<RecommendationSeed>(seeds);
            Tracks = new ImmutableValueArray<SimplifiedTrack>(tracks);
        }

        /// <summary>
        /// Gets or sets the seeds of the <see cref="Recommendations"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="RecommendationSeed"/> objects
        /// representing the seeds of the <see cref="Recommendations"/>.
        /// </returns>
        public IReadOnlyList<RecommendationSeed> Seeds { get; init; }

        /// <summary>
        /// Gets or sets the recommended tracks of the <see cref="Recommendations"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="SimplifiedTrack"/> objects
        /// representing the recommended tracks of the <see cref="Recommendations"/>.
        /// </returns>
        public IReadOnlyList<SimplifiedTrack> Tracks { get; init; }
    }
}
