using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record AudioAnalysis : SpotifyObject
    {
        internal AudioAnalysis(
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
        public IReadOnlyList<TimeInterval> Sections { get; init; }
        public IReadOnlyList<TimeInterval> Segments { get; init; }
        public IReadOnlyList<TimeInterval> Tatums { get; init; }
    }
}