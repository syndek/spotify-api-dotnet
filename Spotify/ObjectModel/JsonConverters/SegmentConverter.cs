using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class SegmentConverter : JsonConverter<Segment>
    {
        internal static readonly SegmentConverter Instance = new();

        private SegmentConverter() : base() { }

        public override Segment Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            Single start = default;
            Single duration = default;
            Single confidence = default;
            Single loudnessStart = default;
            Single loudnessEnd = default;
            Single loudnessMax = default;
            Single loudnessMaxTime = default;
            IReadOnlyList<Single> pitches = Array.Empty<Single>();
            IReadOnlyList<Single> timbre = Array.Empty<Single>();

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
                    case "loudness_start":
                        loudnessStart = reader.ReadSingle();
                        break;
                    case "loudness_end":
                        loudnessEnd = reader.ReadSingle();
                        break;
                    case "loudness_max":
                        loudnessMax = reader.ReadSingle();
                        break;
                    case "loudness_max_time":
                        loudnessMaxTime = reader.ReadSingle();
                        break;
                    case "pitches":
                        reader.Read(JsonTokenType.StartArray);
                        pitches = ArrayConverter<Single>.Instance.Read(ref reader, typeof(IReadOnlyList<Single>), options);
                        break;
                    case "timbre":
                        reader.Read(JsonTokenType.StartArray);
                        timbre = ArrayConverter<Single>.Instance.Read(ref reader, typeof(IReadOnlyList<Single>), options);
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

        public override void Write(Utf8JsonWriter writer, Segment value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}