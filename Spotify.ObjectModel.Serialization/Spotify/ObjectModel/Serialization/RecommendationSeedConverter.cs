using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class RecommendationSeedConverter : JsonConverter<RecommendationSeed>
    {
        public static readonly RecommendationSeedConverter Instance = new();

        private RecommendationSeedConverter() : base() { }

        public override RecommendationSeed Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri? href = null;
            RecommendationSeedType type = default;
            Int32 initialPoolSize = default;
            Int32 afterFilteringSize = default;
            Int32 afterRelinkingSize = default;

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
                    case "id":
                        id = reader.ReadString()!;
                        break;
                    case "href":
                        href = reader.ReadNullableUri();
                        break;
                    case "type":
                        // For some reason, the Spotify Web API returns the type in uppercase. So much for consistency.
                        type = RecommendationSeedTypeConverter.FromSpotifyString(reader.ReadString()!.ToLower());
                        break;
                    case "initialPoolSize":
                        initialPoolSize = reader.ReadInt32();
                        break;
                    case "afterFilteringSize":
                        afterFilteringSize = reader.ReadInt32();
                        break;
                    case "afterRelinkingSize":
                        afterRelinkingSize = reader.ReadInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, href, type, initialPoolSize, afterFilteringSize, afterRelinkingSize);
        }

        public override void Write(Utf8JsonWriter writer, RecommendationSeed value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}