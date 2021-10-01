using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.Authorization.Serialization
{
    internal class AuthenticationErrorConverter : JsonConverter<AuthenticationError>
    {
        public override AuthenticationError Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            string error = string.Empty;
            string? errorDescription = null;

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

                var propertyName = reader.GetString();

                reader.Read(); // Read to next token.

                switch (propertyName)
                {
                    case "error":
                        error = reader.GetString()!;
                        break;
                    case "error_description":
                        errorDescription = reader.GetString();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(error, errorDescription);
        }

        public override void Write(Utf8JsonWriter writer, AuthenticationError value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("error", value.Error);
            writer.WriteString("error_description", value.ErrorDescription);

            writer.WriteEndObject();
        }
    }
}
