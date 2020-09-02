using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class GenreSeedListConverter : JsonConverter<GenreSeedList>
    {
        internal static readonly GenreSeedListConverter Instance = new();

        private GenreSeedListConverter() : base() { }

        public override GenreSeedList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            reader.Read(JsonTokenType.PropertyName); // "genres"
            reader.Read(JsonTokenType.StartArray);
            var genreSeeds = reader.ReadStringArray();

            reader.Read(JsonTokenType.EndObject);

            return new(genreSeeds);
        }

        public override void Write(Utf8JsonWriter writer, GenreSeedList value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}