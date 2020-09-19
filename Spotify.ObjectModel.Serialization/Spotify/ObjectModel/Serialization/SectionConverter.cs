using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class SectionConverter : JsonConverter<Section>
    {
        public static readonly SectionConverter Instance = new();

        private SectionConverter() : base() { }

        public override Section Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            Single start = default;
            Single duration = default;
            Single confidence = default;
            Single loudness = default;
            Single tempo = default;
            Single tempoConfidence = default;
            Int32 key = default;
            Single keyConfidence = default;
            Modality? mode = null;
            Single modeConfidence = default;
            Int32 timeSignature = default;
            Single timeSignatureConfidence = default;

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
                    case "loudness":
                        loudness = reader.ReadSingle();
                        break;
                    case "tempo":
                        tempo = reader.ReadSingle();
                        break;
                    case "tempo_confidence":
                        tempoConfidence = reader.ReadSingle();
                        break;
                    case "key":
                        key = reader.ReadInt32();
                        break;
                    case "key_confidence":
                        keyConfidence = reader.ReadSingle();
                        break;
                    case "mode":
                        var modeValue = reader.ReadInt32();
                        if (modeValue != -1)
                        {
                            mode = (Modality) modeValue;
                        }
                        break;
                    case "mode_confidence":
                        modeConfidence = reader.ReadSingle();
                        break;
                    case "time_signature":
                        timeSignature = reader.ReadInt32();
                        break;
                    case "time_signature_confidence":
                        timeSignatureConfidence = reader.ReadSingle();
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

        public override void Write(Utf8JsonWriter writer, Section value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}