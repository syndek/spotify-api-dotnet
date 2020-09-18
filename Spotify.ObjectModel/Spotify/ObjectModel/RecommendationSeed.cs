using System;

namespace Spotify.ObjectModel
{
    public record RecommendationSeed : IdentifiableObject
    {
        public RecommendationSeed(
            String id,
            Uri? href,
            RecommendationSeedType type,
            Int32 initialPoolSize,
            Int32 afterFilteringSize,
            Int32 afterRelinkingSize) :
            base(id)
        {
            this.Href = href;
            this.Type = type;
            this.InitialPoolSize = initialPoolSize;
            this.AfterFilteringSize = afterFilteringSize;
            this.AfterRelinkingSize = afterRelinkingSize;
        }

        public Uri? Href { get; init; }
        public RecommendationSeedType Type { get; init; }
        public Int32 InitialPoolSize { get; init; }
        public Int32 AfterFilteringSize { get; init; }
        public Int32 AfterRelinkingSize { get; init; }
    }
}