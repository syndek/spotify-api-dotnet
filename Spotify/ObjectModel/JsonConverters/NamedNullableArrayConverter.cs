using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class NamedNullableArrayConverter<TElement> : JsonConverter<IReadOnlyList<TElement?>> where TElement : class
    {
        internal static readonly NamedNullableArrayConverter<TElement> Instance = new();

        private NamedNullableArrayConverter() : base() { }

        public override IReadOnlyList<TElement?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            reader.Read(JsonTokenType.PropertyName);
            reader.Read(JsonTokenType.StartArray);
            var array = NullableArrayConverter<TElement>.Instance.Read(ref reader, typeof(IReadOnlyList<TElement?>), options);

            reader.Read(JsonTokenType.EndObject);

            return array;
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyList<TElement?> value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}