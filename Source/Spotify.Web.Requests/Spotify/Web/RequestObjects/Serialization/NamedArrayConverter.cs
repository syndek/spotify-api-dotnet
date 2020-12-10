using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.RequestObjects.Serialization
{
    internal class NamedArrayConverter<TElement> : JsonConverter<NamedArray<TElement>>
    {
        public override NamedArray<TElement> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var arrayConverter = (JsonConverter<IReadOnlyList<TElement>>) options.GetConverter(typeof(IReadOnlyList<TElement>));

            string name = string.Empty;
            IReadOnlyList<TElement> elements = Array.Empty<TElement>();

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

                name = reader.GetString()!;

                reader.Read(); // Read to next token.

                elements = arrayConverter.Read(ref reader, typeof(IReadOnlyList<TElement>), options)!;
            }

            return new(name, elements);
        }

        public override void Write(Utf8JsonWriter writer, NamedArray<TElement> value, JsonSerializerOptions options)
        {
            var arrayConverter = (JsonConverter<IReadOnlyList<TElement>>) options.GetConverter(typeof(IReadOnlyList<TElement>));

            writer.WriteStartObject();
            writer.WritePropertyName(value.Name);
            arrayConverter.Write(writer, value, options);
            writer.WriteEndObject();
        }
    }
}