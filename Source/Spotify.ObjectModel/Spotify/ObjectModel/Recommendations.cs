using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Recommendations : SpotifyObject
    {
        public Recommendations(IReadOnlyList<RecommendationSeed> seeds, IReadOnlyList<SimplifiedTrack> tracks) : base()
        {
            this.Seeds = seeds;
            this.Tracks = tracks;
        }

        public IReadOnlyList<RecommendationSeed> Seeds { get; init; }
        public IReadOnlyList<SimplifiedTrack> Tracks { get; init; }
    }
}