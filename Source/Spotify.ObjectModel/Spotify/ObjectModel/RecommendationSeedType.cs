using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines types of <see cref="RecommendationSeed"/>.
    /// </summary>
    public enum RecommendationSeedType : Int32
    {
        /// <summary>
        /// The <see cref="RecommendationSeed"/> is an artist seed.
        /// </summary>
        Artist,
        /// <summary>
        /// The <see cref="RecommendationSeed"/> is a track seed.
        /// </summary>
        Track,
        /// <summary>
        /// The <see cref="RecommendationSeed"/> is a genre seed.
        /// </summary>
        Genre
    }
}