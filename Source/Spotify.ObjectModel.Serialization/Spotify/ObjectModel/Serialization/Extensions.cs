using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    internal static class Extensions
    {
        public static JsonConverter<TValue> GetConverter<TValue>(this JsonSerializerOptions options) =>
            (JsonConverter<TValue>) options.GetConverter(typeof(TValue));

        public static DateTime GetReleaseDate(this ref Utf8JsonReader reader)
        {
            var date = reader.GetString()!.Split('-');
            return new(
                date.Length > 0 ? Int32.Parse(date[0]) : 1,
                date.Length > 1 ? Int32.Parse(date[1]) : 1,
                date.Length > 2 ? Int32.Parse(date[2]) : 1);
        }

        public static void WriteReleaseDate(this Utf8JsonWriter writer, DateTime releaseDate, ReleaseDatePrecision releaseDatePrecision)
        {
            var stringValue = releaseDatePrecision switch
            {
                ReleaseDatePrecision.Year => $"{releaseDate.Year}",
                ReleaseDatePrecision.Month => $"{releaseDate.Year}-{releaseDate.Month}",
                ReleaseDatePrecision.Day => $"{releaseDate.Year}-{releaseDate.Month}-{releaseDate.Day}",
                _ => releaseDate.ToString()
            };

            writer.WriteString("release_date", stringValue);
        }
    }
}