using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.Serialization
{
    internal class ErrorConverter : JsonConverter<Error>
    {
        public override Error Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            int status = default;
            string message = string.Empty;

            reader.Read(); // "error"
            reader.Read(); // {

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
                    case "status":
                        status = reader.GetInt32();
                        break;
                    case "message":
                        message = reader.GetString()!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Read(); // }

            return new((HttpStatusCode)status, message);
        }

        public override void Write(Utf8JsonWriter writer, Error value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("error");
            writer.WriteStartObject();

            writer.WriteNumber("status", (int)value.StatusCode);
            writer.WriteString("message", value.Message);

            writer.WriteEndObject();

            writer.WriteEndObject();
        }
    }
}
