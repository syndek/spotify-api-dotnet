using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class CountryCodeConverter : Object
    {
        internal static CountryCode FromSpotifyString(String countryCode) =>
            (countryCode == "from_token") ? CountryCode.FromToken : Enum.Parse<CountryCode>(countryCode);

        internal static String ToSpotifyString(this CountryCode countryCode) =>
            (countryCode == CountryCode.FromToken) ? "from_token" : countryCode.GetName().ToLower();
    }
}