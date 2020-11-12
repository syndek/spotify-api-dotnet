using System;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class CountryCodeConverter
    {
        public static CountryCode FromSpotifyString(String countryCode) =>
            countryCode is "from_token" ? CountryCode.FromToken : Enum.Parse<CountryCode>(countryCode);

        public static String ToSpotifyString(this CountryCode countryCode) =>
            countryCode is CountryCode.FromToken ? "from_token" : countryCode.GetName();
    }
}