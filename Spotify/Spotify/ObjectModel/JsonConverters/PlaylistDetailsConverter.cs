using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class PlaylistDetailsConverter : JsonConverter<PlaylistDetails>
    {
        internal static readonly PlaylistDetailsConverter Instance = new();

        private PlaylistDetailsConverter() : base() { }

        public override PlaylistDetails Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, PlaylistDetails value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("name", value.Name);
            writer.WriteString("description", value.Description);
            if (value.IsPublic is Boolean isPublic)
            {
                writer.WriteBoolean("public", isPublic);
            }
            if (value.IsCollaborative is Boolean isCollaborative)
            {
                writer.WriteBoolean("collaborative", isCollaborative);
            }

            writer.WriteEndObject();
        }
    }
}