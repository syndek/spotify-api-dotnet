using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class NamedArrayConverter<TElement> : JsonConverter<NamedArray<TElement>>
    {
        internal static readonly NamedArrayConverter<TElement> Instance = new();

        private NamedArrayConverter() : base() { }

        public override NamedArray<TElement> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            var name = reader.ReadString()!;

            reader.Read(JsonTokenType.StartArray);
            var elements = reader.ReadArray<TElement>();

            reader.Read(JsonTokenType.EndObject);

            return new(name, elements);
        }

        public override void Write(Utf8JsonWriter writer, NamedArray<TElement> value, JsonSerializerOptions options)
        {
            var elementConverter = (JsonConverter<TElement>) options.GetConverter(typeof(TElement));

            writer.WriteStartObject();

            writer.WritePropertyName(value.Name);
            writer.WriteStartArray();

            foreach (var element in value)
            {
                elementConverter.Write(writer, element, options);
            }

            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}