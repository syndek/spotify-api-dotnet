using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class RecommendationSeedTypeConverter : Object
    {
        internal static RecommendationSeedType FromSpotifyString(String recommendationSeedType) => recommendationSeedType switch
        {
            "artist" => RecommendationSeedType.Artist,
            "track" => RecommendationSeedType.Track,
            "genre" => RecommendationSeedType.Genre,
            _ => throw new ArgumentException(
                $"Invalid {nameof(RecommendationSeedType)} string value: {recommendationSeedType}",
                nameof(recommendationSeedType))
        };

        internal static String ToSpotifyString(this RecommendationSeedType recommendationSeedType) => recommendationSeedType switch
        {
            RecommendationSeedType.Artist => "artist",
            RecommendationSeedType.Track => "track",
            RecommendationSeedType.Genre => "genre",
            _ => throw new ArgumentException(
                $"Invalid {nameof(RecommendationSeedType)} value: {recommendationSeedType}",
                nameof(recommendationSeedType))
        };
    }
}