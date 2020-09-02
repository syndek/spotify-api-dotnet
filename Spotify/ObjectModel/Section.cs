using System;

namespace Spotify.ObjectModel
{
    public record Section : TimeInterval
    {
        internal Section(Single start, Single duration, Single confidence) : base(start, duration, confidence)
        {

        }
    }
}