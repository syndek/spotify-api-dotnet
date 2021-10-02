using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    using RecommendationSeedArray = IReadOnlyList<RecommendationSeed>;
    using SimplifiedTrackArray = IReadOnlyList<SimplifiedTrack>;

    public sealed class RecommendationsConverter : JsonConverter<Recommendations>
    {
        public override Recommendations Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var recommendationSeedArrayConverter = options.GetConverter<RecommendationSeedArray>();
            var simplifiedTrackArrayConverter = options.GetConverter<SimplifiedTrackArray>();

            RecommendationSeedArray seeds = Array.Empty<RecommendationSeed>();
            SimplifiedTrackArray tracks = Array.Empty<SimplifiedTrack>();

            while (reader.Read())
            {
                if (reader.TokenType is JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType is not JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                var propertyName = reader.GetString();

                reader.Read(); // Read to next token.

                switch (propertyName)
                {
                    case "seeds":
                        seeds = recommendationSeedArrayConverter.Read(ref reader, typeof(RecommendationSeedArray), options)!;
                        break;
                    case "tracks":
                        tracks = simplifiedTrackArrayConverter.Read(ref reader, typeof(SimplifiedTrackArray), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(seeds, tracks);
        }

        public override void Write(Utf8JsonWriter writer, Recommendations value, JsonSerializerOptions options)
        {
            var recommendationSeedArrayConverter = options.GetConverter<RecommendationSeedArray>();
            var simplifiedTrackArrayConverter = options.GetConverter<SimplifiedTrackArray>();

            writer.WriteStartObject();
            writer.WritePropertyName("seeds");
            recommendationSeedArrayConverter.Write(writer, value.Seeds, options);
            writer.WritePropertyName("tracks");
            simplifiedTrackArrayConverter.Write(writer, value.Tracks, options);
            writer.WriteEndObject();
        }
    }
}
