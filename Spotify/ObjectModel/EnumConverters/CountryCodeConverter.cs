using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class CountryCodeConverter : Object
    {
        public static CountryCode FromSpotifyString(String countryCode) =>
            (countryCode == "from_token") ? CountryCode.FromToken : Enum.Parse<CountryCode>(countryCode);

        public static String ToSpotifyString(this CountryCode countryCode)
        {
            if (countryCode == CountryCode.FromToken)
            {
                return "from_token";
            }

            return countryCode.GetName().ToLower() ??
                throw new ArgumentException($"Invalid {nameof(CountryCode)} value: {countryCode}", nameof(countryCode));
        }
    }
}