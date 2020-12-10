using System;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class ReleaseDatePrecisionConverter
    {
        public static ReleaseDatePrecision FromSpotifyString(string releaseDatePrecision) => releaseDatePrecision switch
        {
            "year" => ReleaseDatePrecision.Year,
            "month" => ReleaseDatePrecision.Month,
            "day" => ReleaseDatePrecision.Day,
            _ => throw new ArgumentException(
                $"Invalid {nameof(ReleaseDatePrecision)} string value: {releaseDatePrecision}",
                nameof(releaseDatePrecision))
        };

        public static string ToSpotifyString(this ReleaseDatePrecision releaseDatePrecision) => releaseDatePrecision switch
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