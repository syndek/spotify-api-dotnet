using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class ContextConverter : JsonConverter<Context>
    {
        internal static readonly ContextConverter Instance = new();

        private ContextConverter() : base() { }

        public override Context Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Context value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}