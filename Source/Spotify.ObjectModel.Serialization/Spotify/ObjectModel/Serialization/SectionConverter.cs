using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class SectionConverter : JsonConverter<Section>
    {
        public override Section Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            float start = default;
            float duration = default;
            float confidence = default;
            float loudness = default;
            float tempo = default;
            float tempoConfidence = default;
            int key = default;
            float keyConfidence = default;
            Modality? mode = null;
            float modeConfidence = default;
            int timeSignature = default;
            float timeSignatureConfidence = default;

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
                    case "loudness":
                        loudness = reader.GetSingle();
                        break;
                    case "tempo":
                        tempo = reader.GetSingle();
                        break;
                    case "tempo_confidence":
                        tempoConfidence = reader.GetSingle();
                        break;
                    case "key":
                        key = reader.GetInt32();
                        break;
                    case "key_confidence":
                        keyConfidence = reader.GetSingle();
                        break;
                    case "mode":
                        var modeValue = reader.GetInt32();
                        if (modeValue is not -1)
                        {
                            mode = (Modality)modeValue;
                        }
                        break;
                    case "mode_confidence":
                        modeConfidence = reader.GetSingle();
                        break;
                    case "time_signature":
                        timeSignature = reader.GetInt32();
                        break;
                    case "time_signature_confidence":
                        timeSignatureConfidence = reader.GetSingle();
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
                loudness,
                tempo,
                tempoConfidence,
                key,
                keyConfidence,
                mode,
                modeConfidence,
                timeSignature,
                timeSignatureConfidence);
        }

        public override void Write(Utf8JsonWriter writer, Section value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("start", value.Start);
            writer.WriteNumber("duration", value.Duration);
            writer.WriteNumber("confidence", value.Confidence);
            writer.WriteNumber("loudness", value.Loudness);
            writer.WriteNumber("tempo", value.Tempo);
            writer.WriteNumber("tempo_confidence", value.TempoConfidence);
            writer.WriteNumber("key", value.Key);
            writer.WriteNumber("key_confidence", value.KeyConfidence);
            writer.WriteNumber("mode", (int?)value.Mode ?? -1);
            writer.WriteNumber("mode_confidence", value.ModeConfidence);
            writer.WriteNumber("time_signature", value.TimeSignature);
            writer.WriteNumber("time_signature_confidence", value.TimeSignatureConfidence);
            writer.WriteEndObject();
        }
    }
}