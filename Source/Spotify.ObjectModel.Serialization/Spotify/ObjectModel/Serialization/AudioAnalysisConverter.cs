using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    using SectionArray = IReadOnlyList<Section>;
    using SegmentArray = IReadOnlyList<Segment>;
    using TimeIntervalArray = IReadOnlyList<TimeInterval>;

    public sealed class AudioAnalysisConverter : JsonConverter<AudioAnalysis>
    {
        public override AudioAnalysis? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var sectionArrayConverter = options.GetConverter<SectionArray>();
            var segmentArrayConverter = options.GetConverter<SegmentArray>();
            var timeIntervalArrayConverter = options.GetConverter<TimeIntervalArray>();

            TimeIntervalArray bars = Array.Empty<TimeInterval>();
            TimeIntervalArray beats = Array.Empty<TimeInterval>();
            SectionArray sections = Array.Empty<Section>();
            SegmentArray segments = Array.Empty<Segment>();
            TimeIntervalArray tatums = Array.Empty<TimeInterval>();

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
                    case "bars":
                        bars = timeIntervalArrayConverter.Read(ref reader, typeof(TimeIntervalArray), options)!;
                        break;
                    case "beats":
                        beats = timeIntervalArrayConverter.Read(ref reader, typeof(TimeIntervalArray), options)!;
                        break;
                    case "sections":
                        sections = sectionArrayConverter.Read(ref reader, typeof(SectionArray), options)!;
                        break;
                    case "segments":
                        segments = segmentArrayConverter.Read(ref reader, typeof(SegmentArray), options)!;
                        break;
                    case "tatums":
                        tatums = timeIntervalArrayConverter.Read(ref reader, typeof(TimeIntervalArray), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(bars, beats, sections, segments, tatums);
        }

        public override void Write(Utf8JsonWriter writer, AudioAnalysis value, JsonSerializerOptions options)
        {
            var sectionArrayConverter = options.GetConverter<SectionArray>();
            var segmentArrayConverter = options.GetConverter<SegmentArray>();
            var timeIntervalArrayConverter = options.GetConverter<TimeIntervalArray>();

            writer.WriteStartObject();
            writer.WriteString("type", "audio_analysis");
            writer.WritePropertyName("bars");
            timeIntervalArrayConverter.Write(writer, value.Bars, options);
            writer.WritePropertyName("beats");
            timeIntervalArrayConverter.Write(writer, value.Beats, options);
            writer.WritePropertyName("sections");
            sectionArrayConverter.Write(writer, value.Sections, options);
            writer.WritePropertyName("segments");
            segmentArrayConverter.Write(writer, value.Segments, options);
            writer.WritePropertyName("tatums");
            timeIntervalArrayConverter.Write(writer, value.Tatums, options);
            writer.WriteEndObject();
        }
    }
}