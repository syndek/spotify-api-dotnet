using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class ArrayConverter<TElement> : JsonConverter<IReadOnlyList<TElement>>
    {
        internal static readonly ArrayConverter<TElement> Instance = new();

        private ArrayConverter() : base() { }

        public override IReadOnlyList<TElement> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartArray);

            var elements = new List<TElement>();
            var elementType = typeof(TElement);
            var elementConverter = (JsonConverter<TElement>) options.GetConverter(elementType);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return elements.AsReadOnly();
                }

                elements.Add(elementConverter.Read(ref reader, elementType, options)!);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyList<TElement> value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}