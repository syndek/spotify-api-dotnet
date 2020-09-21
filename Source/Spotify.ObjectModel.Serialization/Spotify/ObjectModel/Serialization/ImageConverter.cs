using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class ImageConverter : JsonConverter<Image>
    {
        public override Image? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var uriConverter = options.GetConverter<Uri>();

            Uri url = null!;
            Int32? width = null;
            Int32? height = null;

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
                        if (reader.TryGetInt32(out var widthValue))
                        {
                            width = widthValue;
                        }
                        break;
                    case "height":
                        if (reader.TryGetInt32(out var heightValue))
                        {
                            height = heightValue;
                        }
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(url, width, height);
        }

        public override void Write(Utf8JsonWriter writer, Image value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}