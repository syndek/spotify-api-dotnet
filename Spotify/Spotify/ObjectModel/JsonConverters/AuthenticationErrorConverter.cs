using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class AuthenticationErrorConverter : JsonConverter<AuthenticationError>
    {
        internal static readonly AuthenticationErrorConverter Instance = new();

        private AuthenticationErrorConverter() : base() { }

        public override AuthenticationError Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            reader.Read(JsonTokenType.PropertyName); // "error"
            var error = reader.ReadString()!;

            String? errorDescription = null;

            reader.Read();

            if (reader.TokenType != JsonTokenType.EndObject)
            {
                reader.AssertTokenType(JsonTokenType.PropertyName); // "error_description"
                errorDescription = reader.ReadString();
            }

            reader.Read(JsonTokenType.EndObject);

            return new(error, errorDescription);
        }

        public override void Write(Utf8JsonWriter writer, AuthenticationError value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}