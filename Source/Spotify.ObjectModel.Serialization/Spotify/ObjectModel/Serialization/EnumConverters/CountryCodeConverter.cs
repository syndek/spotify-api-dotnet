using System;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class CountryCodeConverter : Object
    {
        public static CountryCode FromSpotifyString(String countryCode) =>
            (countryCode == "from_token") ? CountryCode.FromToken : Enum.Parse<CountryCode>(countryCode);

        public static String ToSpotifyString(this CountryCode countryCode) =>
            (countryCode == CountryCode.FromToken) ? "from_token" : countryCode.GetName();
    }
}