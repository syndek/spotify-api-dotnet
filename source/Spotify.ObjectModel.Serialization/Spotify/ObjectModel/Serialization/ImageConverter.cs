using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class ImageConverter : JsonConverter<Image>
    {
        public override Image Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var uriConverter = options.GetConverter<Uri>();

            Uri url = null!;
            int? width = null;
            int? height = null;

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
                    case "url":
                        url = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "width":
                        if (reader.TokenType is not JsonTokenType.Null)
                        {
                            width = reader.GetInt32();
                        }
                        break;
                    case "height":
                        if (reader.TokenType is not JsonTokenType.Null)
                        {
                            height = reader.GetInt32();
                        }
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(url, width, height);
        }

        public override void Write(Utf8JsonWriter writer, Image value, JsonSerializerOptions options)
        {
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WritePropertyName("url");
            uriConverter.Write(writer, value.Url, options);

            if (value.Width is not null)
            {
                writer.WriteNumber("width", value.Width.Value);
            }
            else
            {
                writer.WriteNull("width");
            }

            if (value.Height is not null)
            {
                writer.WriteNumber("height", value.Height.Value);
            }
            else
            {
                writer.WriteNull("height");
            }

            writer.WriteEndObject();
        }
    }
}
