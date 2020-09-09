using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class CategoryConverter : JsonConverter<Category>
    {
        internal static readonly CategoryConverter Instance = new();

        private CategoryConverter() : base() { }

        public override Category Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri href = null!;
            String name = String.Empty;
            IReadOnlyList<Image> images = Array.Empty<Image>();

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
                        href = reader.ReadUri();
                        break;
                    case "name":
                        name = reader.ReadString()!;
                        break;
                    case "icons":
                        reader.Read(JsonTokenType.StartArray);
                        images = ArrayConverter<Image>.Instance.Read(ref reader, typeof(IReadOnlyList<Image>), options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, href, name, images);
        }

        public override void Write(Utf8JsonWriter writer, Category value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}