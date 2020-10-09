using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.RequestObjects.Serialization
{
    internal class PlaylistDetailsConverter : JsonConverter<PlaylistDetails>
    {
        public override PlaylistDetails Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            String? name = null;
            String? description = null;
            Boolean? isPublic = null;
            Boolean? isCollaborative = null;

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
                    case "name":
                        name = reader.GetString();
                        break;
                    case "description":
                        description = reader.GetString();
                        break;
                    case "public":
                        isPublic = reader.GetBoolean();
                        break;
                    case "collaborative":
                        isCollaborative = reader.GetBoolean();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(name, description, isPublic, isCollaborative);
        }

        public override void Write(Utf8JsonWriter writer, PlaylistDetails value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("name", value.Name);
            writer.WriteString("description", value.Description);

            if (value.IsPublic is Boolean isPublic)
            {
                writer.WriteBoolean("public", isPublic);
            }
            else
            {
                writer.WriteNull("public");
            }

            if (value.IsCollaborative is Boolean isCollaborative)
            {
                writer.WriteBoolean("collaborative", isCollaborative);
            }
            else
            {
                writer.WriteNull("collaborative");
            }

            writer.WriteEndObject();
        }
    }
}