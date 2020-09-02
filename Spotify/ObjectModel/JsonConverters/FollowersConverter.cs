using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class FollowersConverter : JsonConverter<Followers>
    {
        internal static readonly FollowersConverter Instance = new();

        private FollowersConverter() : base() { }

        public override Followers Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String? href = null;
            Int32 total = default;

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
                    case "href":
                        href = reader.ReadString();
                        break;
                    case "total":
                        total = reader.ReadInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(href, total);
        }

        public override void Write(Utf8JsonWriter writer, Followers value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}