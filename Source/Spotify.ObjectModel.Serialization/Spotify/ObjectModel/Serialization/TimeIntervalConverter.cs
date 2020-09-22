using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class TimeIntervalConverter : JsonConverter<TimeInterval>
    {
        public override TimeInterval? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            Single start = default;
            Single duration = default;
            Single confidence = default;

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
                    case "start":
                        start = reader.GetSingle();
                        break;
                    case "duration":
                        duration = reader.GetSingle();
                        break;
                    case "confidence":
                        confidence = reader.GetSingle();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(start, duration, confidence);
        }

        public override void Write(Utf8JsonWriter writer, TimeInterval value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("start", value.Start);
            writer.WriteNumber("duration", value.Duration);
            writer.WriteNumber("confidence", value.Confidence);
            writer.WriteEndObject();
        }
    }
}