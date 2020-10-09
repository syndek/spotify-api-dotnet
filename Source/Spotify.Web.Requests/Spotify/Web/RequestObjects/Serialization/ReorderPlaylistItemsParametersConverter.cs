using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.RequestObjects.Serialization
{
    internal class ReorderPlaylistItemsParametersConverter : JsonConverter<ReorderPlaylistItemsParameters>
    {
        public override ReorderPlaylistItemsParameters Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            Int32 rangeStart = default;
            Int32 insertBefore = default;
            Int32? rangeLength = null;
            String? snapshotId = null;

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
                    case "range_start":
                        rangeStart = reader.GetInt32();
                        break;
                    case "insert_before":
                        insertBefore = reader.GetInt32();
                        break;
                    case "range_length":
                        rangeLength = (reader.TokenType is JsonTokenType.Null) ? null : reader.GetInt32();
                        break;
                    case "snapshot_id":
                        snapshotId = reader.GetString();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(rangeStart, insertBefore, rangeLength, snapshotId);
        }

        public override void Write(Utf8JsonWriter writer, ReorderPlaylistItemsParameters value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("range_start", value.RangeStart);
            writer.WriteNumber("insert_before", value.InsertBefore);

            if (value.RangeLength is Int32 rangeLength)
            {
                writer.WriteNumber("range_length", rangeLength);
            }
            else
            {
                writer.WriteNull("range_length");
            }

            writer.WriteString("snapshot_id", value.SnapshotId);

            writer.WriteEndObject();
        }
    }
}