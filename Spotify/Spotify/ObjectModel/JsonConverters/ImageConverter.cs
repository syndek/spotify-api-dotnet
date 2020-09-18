using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class ImageConverter : JsonConverter<Image>
    {
        internal static readonly ImageConverter Instance = new();

        private ImageConverter() : base() { }

        public override Image Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            Uri url = null!;
            Int32? width = null;
            Int32? height = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                switch (reader.GetString())
                {
                    case "url":
                        url = reader.ReadUri();
                        break;
                    case "width":
                        reader.Read();
                        if (reader.TryGetInt32(out var widthValue))
                        {
                            width = widthValue;
                        }
                        break;
                    case "height":
                        reader.Read();
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