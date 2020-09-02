using System;

namespace Spotify.ObjectModel
{
    public record Segment : TimeInterval
    {
        internal Segment(Single start, Single duration, Single confidence) : base(start, duration, confidence)
        {

        }
    }
}