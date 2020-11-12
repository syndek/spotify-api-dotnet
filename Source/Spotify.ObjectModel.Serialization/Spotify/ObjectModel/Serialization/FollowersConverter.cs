using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class FollowersConverter : JsonConverter<Followers>
    {
        public override Followers Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var uriConverter = options.GetConverter<Uri>();

            Uri? href = null;
            Int32 total = default;

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
                    case "href":
                        href = (reader.TokenType is JsonTokenType.Null) ? null : uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "total":
                        total = reader.GetInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(href, total);
        }

        public override void Write(Utf8JsonWriter writer, Followers value, JsonSerializerOptions options)
        {
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            if (value.Href is not null)
            {
                writer.WritePropertyName("href");
                uriConverter.Write(writer, value.Href, options);
            }
            else
            {
                writer.WriteNull("href");
            }
            writer.WriteNumber("total", value.Total);
            writer.WriteEndObject();
        }
    }
}