using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class CopyrightTypeConverter : Object
    {
        internal static CopyrightType FromSpotifyString(String copyrightType) => copyrightType switch
        {
            "C" => CopyrightType.Copyright,
            "P" => CopyrightType.PerformanceCopyright,
            _ => throw new ArgumentException($"Invalid {nameof(CopyrightType)} string value: {copyrightType}", nameof(copyrightType))
        };

        internal static String ToSpotifyString(this CopyrightType copyrightType) => copyrightType switch
        {
            CopyrightType.Copyright => "C",
            CopyrightType.PerformanceCopyright => "P",
            _ => throw new ArgumentException($"Invalid {nameof(CopyrightType)} value: {copyrightType}", nameof(copyrightType))
        };
    }
}