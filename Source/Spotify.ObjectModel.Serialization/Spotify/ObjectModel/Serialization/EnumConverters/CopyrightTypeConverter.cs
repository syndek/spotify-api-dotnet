using System;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class CopyrightTypeConverter : Object
    {
        public static CopyrightType FromSpotifyString(String copyrightType) => copyrightType switch
        {
            "C" => CopyrightType.Copyright,
            "P" => CopyrightType.PerformanceCopyright,
            _ => throw new ArgumentException($"Invalid {nameof(CopyrightType)} string value: {copyrightType}", nameof(copyrightType))
        };

        public static String ToSpotifyString(this CopyrightType copyrightType) => copyrightType switch
        {
            CopyrightType.Copyright => "C",
            CopyrightType.PerformanceCopyright => "P",
            _ => throw new ArgumentException($"Invalid {nameof(CopyrightType)} value: {copyrightType}", nameof(copyrightType))
        };
    }
}