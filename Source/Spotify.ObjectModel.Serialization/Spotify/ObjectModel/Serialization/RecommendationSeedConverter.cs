using Spotify.ObjectModel.Serialization.EnumConverters;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class RecommendationSeedConverter : JsonConverter<RecommendationSeed>
    {
        public override RecommendationSeed Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var uriConverter = options.GetConverter<Uri>();

            string id = string.Empty;
            Uri? href = null;
            RecommendationSeedType type = default;
            int initialPoolSize = default;
            int afterFilteringSize = default;
            int afterRelinkingSize = default;

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
                    case "id":
                        id = reader.GetString()!;
                        break;
                    case "href":
                        href = reader.TokenType is JsonTokenType.Null ? null : uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "type":
                        // For some reason, the Spotify Web API returns the type in uppercase. So much for consistency.
                        type = RecommendationSeedTypeConverter.FromSpotifyString(reader.GetString()!.ToLower());
                        break;
                    case "initialPoolSize":
                        initialPoolSize = reader.GetInt32();
                        break;
                    case "afterFilteringSize":
                        afterFilteringSize = reader.GetInt32();
                        break;
                    case "afterRelinkingSize":
                        afterRelinkingSize = reader.GetInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, href, type, initialPoolSize, afterFilteringSize, afterRelinkingSize);
        }

        public override void Write(Utf8JsonWriter writer, RecommendationSeed value, JsonSerializerOptions options)
        {
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("id", value.Id);

            if (value.Href is not null)
            {
                writer.WritePropertyName("href");
                uriConverter.Write(writer, value.Href, options);
            }
            else
            {
                writer.WriteNull("href");
            }

            writer.WriteString("type", value.Type.ToSpotifyString());
            writer.WriteNumber("initialPoolSize", value.InitialPoolSize);
            writer.WriteNumber("afterFilteringSize", value.AfterFilteringSize);
            writer.WriteNumber("afterRelinkingSize", value.AfterRelinkingSize);
            writer.WriteEndObject();
        }
    }
}
