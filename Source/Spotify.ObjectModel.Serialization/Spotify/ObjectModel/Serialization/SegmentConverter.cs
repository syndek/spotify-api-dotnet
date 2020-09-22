using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    using SingleArray = IReadOnlyList<Single>;

    public sealed class SegmentConverter : JsonConverter<Segment>
    {
        public override Segment? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var singleArrayConverter = options.GetConverter<SingleArray>();

            Single start = default;
            Single duration = default;
            Single confidence = default;
            Single loudnessStart = default;
            Single loudnessEnd = default;
            Single loudnessMax = default;
            Single loudnessMaxTime = default;
            SingleArray pitches = Array.Empty<Single>();
            SingleArray timbre = Array.Empty<Single>();

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
                    case "loudness_start":
                        loudnessStart = reader.GetSingle();
                        break;
                    case "loudness_end":
                        loudnessEnd = reader.GetSingle();
                        break;
                    case "loudness_max":
                        loudnessMax = reader.GetSingle();
                        break;
                    case "loudness_max_time":
                        loudnessMaxTime = reader.GetSingle();
                        break;
                    case "pitches":
                        pitches = singleArrayConverter.Read(ref reader, typeof(SingleArray), options)!;
                        break;
                    case "timbre":
                        timbre = singleArrayConverter.Read(ref reader, typeof(SingleArray), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(
                start,
                duration,
                confidence,
                loudnessStart,
                loudnessEnd,
                loudnessMax,
                loudnessMaxTime,
                pitches,
                timbre);
        }

        public override void Write(Utf8JsonWriter writer, Segment value, JsonSerializerOptions options)
        {
            var singleArrayConverter = options.GetConverter<SingleArray>();

            writer.WriteStartObject();
            writer.WriteNumber("start", value.Start);
            writer.WriteNumber("duration", value.Duration);
            writer.WriteNumber("confidence", value.Confidence);
            writer.WriteNumber("loudness_start", value.LoudnessStart);
            writer.WriteNumber("loudness_end", value.LoudnessEnd);
            writer.WriteNumber("loudness_max", value.LoudnessMax);
            writer.WriteNumber("loudness_max_time", value.LoudnessMaxTime);
            writer.WritePropertyName("pitches");
            singleArrayConverter.Write(writer, value.Pitches, options);
            writer.WritePropertyName("timbre");
            singleArrayConverter.Write(writer, value.Timbre, options);
            writer.WriteEndObject();
        }
    }
}