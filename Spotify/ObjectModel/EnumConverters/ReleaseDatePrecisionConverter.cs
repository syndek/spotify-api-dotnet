using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class ReleaseDatePrecisionConverter : Object
    {
        internal static ReleaseDatePrecision FromSpotifyString(String releaseDatePrecision) => releaseDatePrecision switch
        {
            "year" => ReleaseDatePrecision.Year,
            "month" => ReleaseDatePrecision.Month,
            "day" => ReleaseDatePrecision.Day,
            _ => throw new ArgumentException(
                $"Invalid {nameof(ReleaseDatePrecision)} string value: {releaseDatePrecision}",
                nameof(releaseDatePrecision))
        };

        internal static String ToSpotifyString(this ReleaseDatePrecision releaseDatePrecision) => releaseDatePrecision switch
        {
            ReleaseDatePrecision.Year => "year",
            ReleaseDatePrecision.Month => "month",
            ReleaseDatePrecision.Day => "day",
            _ => throw new ArgumentException(
                $"Invalid {nameof(ReleaseDatePrecision)} value: {releaseDatePrecision}",
                nameof(releaseDatePrecision))
        };
    }
}