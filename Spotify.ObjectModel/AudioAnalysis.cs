using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record AudioAnalysis : SpotifyObject
    {
        public AudioAnalysis(
            IReadOnlyList<TimeInterval> bars,
            IReadOnlyList<TimeInterval> beats,
            IReadOnlyList<Section> sections,
            IReadOnlyList<Segment> segments,
            IReadOnlyList<TimeInterval> tatums) : base()
        {
            this.Bars = bars;
            this.Beats = beats;
            this.Sections = sections;
            this.Segments = segments;
            this.Tatums = tatums;
        }

        public IReadOnlyList<TimeInterval> Bars { get; init; }
        public IReadOnlyList<TimeInterval> Beats { get; init; }
        public IReadOnlyList<Section> Sections { get; init; }
        public IReadOnlyList<Segment> Segments { get; init; }
        public IReadOnlyList<TimeInterval> Tatums { get; init; }
    }
}