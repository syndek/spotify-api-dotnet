using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class SimplifiedPlaylistConverter : JsonConverter<SimplifiedPlaylist>
    {
        internal static readonly SimplifiedPlaylistConverter Instance = new();

        private SimplifiedPlaylistConverter() : base() { }

        public override SimplifiedPlaylist Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            String? description = null;
            IReadOnlyList<Image> images = Array.Empty<Image>();
            PublicUser owner = null!;
            Paging<PlaylistTrack> tracks = Paging<PlaylistTrack>.Empty;
            Boolean? isPublic = null;
            Boolean isCollaborative = default;
            String snapshotId = String.Empty;
            IReadOnlyDictionary<String, Uri> externalUrls = null!;

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
                    case "id":
                        id = reader.ReadString()!;
                        break;
                    case "uri":
                        uri = reader.ReadUri();
                        break;
                    case "href":
                        href = reader.ReadUri();
                        break;
                    case "name":
                        name = reader.ReadString()!;
                        break;
                    case "description":
                        description = reader.ReadString();
                        break;
                    case "images":
                        reader.Read(JsonTokenType.StartArray);
                        images = reader.ReadArray<Image>();
                        break;
                    case "owner":
                        reader.Read(JsonTokenType.StartObject);
                        owner = PublicUserConverter.Instance.Read(ref reader, typeof(PublicUser), options);
                        break;
                    case "tracks":
                        reader.Read(JsonTokenType.StartObject);
                        tracks = reader.ReadPaging<PlaylistTrack>();
                        break;
                    case "public":
                        reader.Read();
                        isPublic = reader.TokenType switch
                        {
                            JsonTokenType.Null => null,
                            JsonTokenType.True => true,
                            JsonTokenType.False => false,
                            _ => throw new JsonException("Invalid public value.")
                        };
                        break;
                    case "collaborative":
                        isCollaborative = reader.ReadBoolean();
                        break;
                    case "snapshot_id":
                        snapshotId = reader.ReadString()!;
                        break;
                    case "external_urls":
                        reader.Read(JsonTokenType.StartObject);
                        externalUrls = reader.ReadExternalUrls();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(
                id,
                uri,
                href,
                name,
                description,
                images,
                owner,
                tracks,
                isPublic,
                isCollaborative,
                snapshotId,
                externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, SimplifiedPlaylist value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}
