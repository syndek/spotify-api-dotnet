using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class SavedConverter<TSaved> : JsonConverter<Saved<TSaved>> where TSaved : ISaveable
    {
        public override Saved<TSaved>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var savedObjectConverter = options.GetConverter<TSaved>();
            var savedObjectType = typeof(TSaved);

            DateTime savedAt = default;
            TSaved savedObject = default;

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
                    case "added_at":
                        savedAt = reader.GetDateTime();
                        break;
                    default:
                        if (propertyName is not null && propertyName.Equals(savedObjectType.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            savedObject = savedObjectConverter.Read(ref reader, savedObjectType, options);
                        }
                        else
                        {
                            reader.Skip();
                        }
                        break;
                }
            }

            return new(savedObject!, savedAt);
        }

        public override void Write(Utf8JsonWriter writer, Saved<TSaved> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("added_at", value.SavedAt);
            writer.WritePropertyName(typeof(TSaved).Name.ToLower());
            options.GetConverter<TSaved>().Write(writer, value.SavedObject, options);
            writer.WriteEndObject();
        }
    }
}