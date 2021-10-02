using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class CopyrightConverter : JsonConverter<Copyright>
    {
        public override Copyright Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            string text = string.Empty;
            CopyrightType type = default;

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
                    case "text":
                        text = reader.GetString()!;
                        break;
                    case "type":
                        type = CopyrightTypeConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(text, type);
        }

        public override void Write(Utf8JsonWriter writer, Copyright value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("text", value.Text);
            writer.WriteString("type", value.Type.ToSpotifyString());
            writer.WriteEndObject();
        }
    }
}
