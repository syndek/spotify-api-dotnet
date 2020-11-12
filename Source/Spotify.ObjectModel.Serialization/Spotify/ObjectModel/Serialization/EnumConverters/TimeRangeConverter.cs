using System;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class TimeRangeConverter
    {
        public static TimeRange FromSpotifyString(String timeRange) => timeRange switch
        {
            "short_term" => TimeRange.ShortTerm,
            "medium_term" => TimeRange.MediumTerm,
            "long_term" => TimeRange.LongTerm,
            _ => throw new ArgumentException($"Invalid {nameof(TimeRange)} string value: {timeRange}", nameof(timeRange))
        };

        public static String ToSpotifyString(this TimeRange timeRange) => timeRange switch
        {
            TimeRange.ShortTerm => "short_term",
            TimeRange.MediumTerm => "medium_term",
            TimeRange.LongTerm => "long_term",
            _ => throw new ArgumentException($"Invalid {nameof(TimeRange)} value: {timeRange}", nameof(timeRange))
        };
    }
}