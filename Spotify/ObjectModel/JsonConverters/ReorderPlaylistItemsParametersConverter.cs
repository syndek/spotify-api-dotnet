using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class ReorderPlaylistItemsParametersConverter : JsonConverter<ReorderPlaylistItemsParameters>
    {
        internal static readonly ReorderPlaylistItemsParametersConverter Instance = new();

        private ReorderPlaylistItemsParametersConverter() : base() { }

        public override ReorderPlaylistItemsParameters Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, ReorderPlaylistItemsParameters value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("range_start", value.RangeStart);
            writer.WriteNumber("insert_before", value.InsertBefore);
            if (value.RangeLength is Int32 rangeLength)
            {
                writer.WriteNumber("range_length", rangeLength);
            }
            writer.WriteString("snapshot_id", value.SnapshotId);

            writer.WriteEndObject();
        }
    }
}