using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class RecommendationsConverter : JsonConverter<Recommendations>
    {
        public static readonly RecommendationsConverter Instance = new();

        private RecommendationsConverter() : base() { }

        public override Recommendations Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            IReadOnlyList<RecommendationSeed> seeds = Array.Empty<RecommendationSeed>();
            IReadOnlyList<SimplifiedTrack> tracks = Array.Empty<SimplifiedTrack>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                switch (reader.GetString())
                {
                    case "seeds":
                        reader.Read(JsonTokenType.StartArray);
                        seeds = reader.ReadArray<RecommendationSeed>();
                        break;
                    case "tracks":
                        reader.Read(JsonTokenType.StartArray);
                        tracks = reader.ReadArray<SimplifiedTrack>();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(seeds, tracks);
        }

        public override void Write(Utf8JsonWriter writer, Recommendations value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}