using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class ErrorConverter : JsonConverter<Error>
    {
        internal static readonly ErrorConverter Instance = new();

        private ErrorConverter() : base() { }

        public override Error Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            reader.Read(JsonTokenType.PropertyName); // "error"
            reader.Read(JsonTokenType.StartObject);

            reader.Read(JsonTokenType.PropertyName); // "status"
            var status = reader.ReadInt32();

            reader.Read(JsonTokenType.PropertyName); // "message"
            var message = reader.ReadString()!;

            reader.Read(JsonTokenType.EndObject);

            reader.Read(JsonTokenType.EndObject);

            return new Error(status, message);
        }

        public override void Write(Utf8JsonWriter writer, Error value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}