using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class NamedArrayConverter<TElement> : JsonConverter<IReadOnlyList<TElement>>
    {
        internal static readonly NamedArrayConverter<TElement> Instance = new();

        private NamedArrayConverter() : base() { }

        public override IReadOnlyList<TElement> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            reader.Read(JsonTokenType.PropertyName);
            reader.Read(JsonTokenType.StartArray);
            var array = reader.ReadArray<TElement>();

            reader.Read(JsonTokenType.EndObject);

            return array;
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyList<TElement> value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}