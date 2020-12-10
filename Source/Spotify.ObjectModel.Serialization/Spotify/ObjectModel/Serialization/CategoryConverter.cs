using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    using ImageArray = IReadOnlyList<Image>;

    public sealed class CategoryConverter : JsonConverter<Category>
    {
        public override Category Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var imageArrayConverter = options.GetConverter<ImageArray>();
            var uriConverter = options.GetConverter<Uri>();

            string id = string.Empty;
            Uri href = null!;
            string name = string.Empty;
            ImageArray images = Array.Empty<Image>();

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
                        href = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "name":
                        name = reader.GetString()!;
                        break;
                    case "icons":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, href, name, images);
        }

        public override void Write(Utf8JsonWriter writer, Category value, JsonSerializerOptions options)
        {
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("name", value.Name);
            writer.WritePropertyName("icons");
            imageArrayConverter.Write(writer, value.Icons, options);
            writer.WriteEndObject();
        }
    }
}