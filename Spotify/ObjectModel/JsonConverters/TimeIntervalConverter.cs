using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class TimeIntervalConverter : JsonConverter<TimeInterval>
    {
        internal static readonly TimeIntervalConverter Instance = new();

        private TimeIntervalConverter() : base() { }

        public override TimeInterval Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            Single start = default;
            Single duration = default;
            Single confidence = default;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                switch (reader.GetString())
                {
                    case "start":
                        start = reader.ReadSingle();
                        break;
                    case "duration":
                        duration = reader.ReadSingle();
                        break;
                    case "confidence":
                        confidence = reader.ReadSingle();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(start, duration, confidence);
        }

        public override void Write(Utf8JsonWriter writer, TimeInterval value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}