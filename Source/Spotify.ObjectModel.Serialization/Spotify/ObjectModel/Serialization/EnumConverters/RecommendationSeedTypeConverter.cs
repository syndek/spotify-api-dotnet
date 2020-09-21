using System;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class RecommendationSeedTypeConverter : Object
    {
        public static RecommendationSeedType FromSpotifyString(String recommendationSeedType) => recommendationSeedType switch
        {
            "artist" => RecommendationSeedType.Artist,
            "track" => RecommendationSeedType.Track,
            "genre" => RecommendationSeedType.Genre,
            _ => throw new ArgumentException(
                $"Invalid {nameof(RecommendationSeedType)} string value: {recommendationSeedType}",
                nameof(recommendationSeedType))
        };

        public static String ToSpotifyString(this RecommendationSeedType recommendationSeedType) => recommendationSeedType switch
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