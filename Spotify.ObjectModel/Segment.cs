using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Segment : TimeInterval
    {
        public Segment(
            Single start,
            Single duration,
            Single confidence,
            Single loudnessStart,
            Single loudnessEnd,
            Single loudnessMax,
            Single loudnessMaxTime,
            IReadOnlyList<Single> pitches,
            IReadOnlyList<Single> timbre) :
            base(start, duration, confidence)
        {
            this.LoudnessStart = loudnessStart;
            this.LoudnessEnd = loudnessEnd;
            this.LoudnessMax = loudnessMax;
            this.LoudnessMaxTime = loudnessMaxTime;
            this.Pitches = pitches;
            this.Timbre = timbre;
        }

        public Single LoudnessStart { get; init; }
        public Single LoudnessEnd { get; init; }
        public Single LoudnessMax { get; init; }
        public Single LoudnessMaxTime { get; init; }
        public IReadOnlyList<Single> Pitches { get; init; }
        public IReadOnlyList<Single> Timbre { get; init; }
    }
}