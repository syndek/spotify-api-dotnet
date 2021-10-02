using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a seed from a <see cref="Recommendations"/> object.
    /// </summary>
    public record RecommendationSeed : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationSeed"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the ID used to select the seed.</param>
        /// <param name="href">
        /// A link to the full track or artist data for the seed,
        /// or <see langword="null"/> if the seed is a genre seed.
        /// </param>
        /// <param name="type">The type of the seed.</param>
        /// <param name="initialPoolSize">
        /// An <see cref="int"/> representing the number of recommended tracks available for the seed.
        /// </param>
        /// <param name="afterFilteringSize">
        /// An <see cref="int"/> representing the number of tracks available after min and max filters have been applied.
        /// </param>
        /// <param name="afterRelinkingSize">
        /// An <see cref="int"/> representing the number of tracks available after relinking for regional availability.
        /// </param>
        public RecommendationSeed(
            string id,
            Uri? href,
            RecommendationSeedType type,
            int initialPoolSize,
            int afterFilteringSize,
            int afterRelinkingSize)
        {
            Id = id;
            Href = href;
            Type = type;
            InitialPoolSize = initialPoolSize;
            AfterFilteringSize = afterFilteringSize;
            AfterRelinkingSize = afterRelinkingSize;
        }

        /// <summary>
        /// Gets or sets the ID used to select the <see cref="RecommendationSeed"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the ID used to select the seed.</returns>
        public string Id { get; init; }

        /// <summary>
        /// Gets or sets a link to the full track or artist data for the <see cref="RecommendationSeed"/>.
        /// </summary>
        /// <remarks>
        /// For tracks, this will be a link to a <see cref="Track"/> object.
        /// For artists, this will be a link to an <see cref="Artist"/> object.
        /// </remarks>
        /// <returns>
        /// A link to the full track or artist data for the <see cref="RecommendationSeed"/>,
        /// or <see langword="null"/> if the seed is a genre seed.
        /// </returns>
        public Uri? Href { get; init; }

        /// <summary>
        /// Gets or sets the type of the <see cref="RecommendationSeed"/>.
        /// </summary>
        /// <returns>The type of the <see cref="RecommendationSeed"/>.</returns>
        public RecommendationSeedType Type { get; init; }

        /// <summary>
        /// Gets or sets the number of recommended tracks available for the seed.
        /// </summary>
        /// <returns>
        /// An <see cref="int"/> representing the number of recommended tracks available for the seed.
        /// </returns>
        public int InitialPoolSize { get; init; }

        /// <summary>
        /// Gets or sets the number of tracks available after min and max filters have been applied.
        /// </summary>
        /// <returns>
        /// An <see cref="int"/> representing the number of tracks available after min and max filters have been applied.
        /// </returns>
        public int AfterFilteringSize { get; init; }

        /// <summary>
        /// Gets or sets the number of tracks available after relinking for regional availability.
        /// </summary>
        /// <returns>
        /// An <see cref="int"/> representing the number of tracks available after relinking for regional availability.
        /// </returns>
        public int AfterRelinkingSize { get; init; }
    }
}
