using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class ResumePointConverter : JsonConverter<ResumePoint>
    {
        internal static readonly ResumePointConverter Instance = new();

        private ResumePointConverter() : base() { }

        public override ResumePoint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            Int32 resumePosition = default;
            Boolean isFullyPlayed = default;

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
                    case "resume_position_ms":
                        resumePosition = reader.ReadInt32();
                        break;
                    case "fully_played":
                        isFullyPlayed = reader.ReadBoolean();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(resumePosition, isFullyPlayed);
        }

        public override void Write(Utf8JsonWriter writer, ResumePoint value, JsonSerializerOptions options) => throw new NotImplementedException();
    }
}