using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class ResumePointConverter : JsonConverter<ResumePoint>
    {
        public override ResumePoint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            Int32 resumePosition = default;
            Boolean isFullyPlayed = default;

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
                    case "resume_position_ms":
                        resumePosition = reader.GetInt32();
                        break;
                    case "fully_played":
                        isFullyPlayed = reader.GetBoolean();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(resumePosition, isFullyPlayed);
        }

        public override void Write(Utf8JsonWriter writer, ResumePoint value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("resume_position_ms", value.ResumePosition);
            writer.WriteBoolean("fully_played", value.IsFullyPlayed);
            writer.WriteEndObject();
        }
    }
}