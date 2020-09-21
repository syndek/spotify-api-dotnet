using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class CountryCodeConverter : JsonConverter<CountryCode>
    {
        public override CountryCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return EnumConverters.CountryCodeConverter.FromSpotifyString(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, CountryCode value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToSpotifyString());
        }
    }
}