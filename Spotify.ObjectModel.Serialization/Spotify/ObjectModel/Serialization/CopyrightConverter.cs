using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class CopyrightConverter : JsonConverter<Copyright>
    {
        public static readonly CopyrightConverter Instance = new();

        private CopyrightConverter() : base() { }

        public override Copyright Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String text = String.Empty;
            CopyrightType type = default;

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
                    case "text":
                        text = reader.ReadString()!;
                        break;
                    case "type":
                        type = CopyrightTypeConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(text, type);
        }

        public override void Write(Utf8JsonWriter writer, Copyright value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}