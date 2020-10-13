using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel.Serialization
{
    using ExternalUrls = IReadOnlyDictionary<String, Uri>;
    using ImageArray = IReadOnlyList<Image>;

    public sealed class SimplifiedPlaylistConverter : JsonConverter<SimplifiedPlaylist>
    {
        public override SimplifiedPlaylist? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var playlistTrackPagingConverter = options.GetConverter<Paging<PlaylistTrack>>();
            var publicUserConverter = options.GetConverter<PublicUser>();
            var uriConverter = options.GetConverter<Uri>();

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            String? description = null;
            ImageArray images = Array.Empty<Image>();
            PublicUser owner = null!;
            Paging<PlaylistTrack> tracks = Paging<PlaylistTrack>.Empty;
            Boolean? isPublic = null;
            Boolean isCollaborative = default;
            String snapshotId = String.Empty;
            ExternalUrls externalUrls = null!;

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
                    case "id":
                        id = reader.GetString()!;
                        break;
                    case "uri":
                        uri = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "href":
                        href = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "name":
                        name = reader.GetString()!;
                        break;
                    case "description":
                        description = reader.GetString();
                        break;
                    case "images":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    case "owner":
                        owner = publicUserConverter.Read(ref reader, typeof(PublicUser), options)!;
                        break;
                    case "tracks":
                        tracks = playlistTrackPagingConverter.Read(ref reader, typeof(Paging<PlaylistTrack>), options)!;
                        break;
                    case "public":
                        reader.Read();
                        isPublic = reader.TokenType switch
                        {
                            JsonTokenType.Null => null,
                            JsonTokenType.True => true,
                            JsonTokenType.False => false,
                            _ => throw new JsonException()
                        };
                        break;
                    case "collaborative":
                        isCollaborative = reader.GetBoolean();
                        break;
                    case "snapshot_id":
                        snapshotId = reader.GetString()!;
                        break;
                    case "external_urls":
                        externalUrls = externalUrlsConverter.Read(ref reader, typeof(ExternalUrls), options)!;
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

        public override void Write(Utf8JsonWriter writer, SimplifiedPlaylist value, JsonSerializerOptions options)
        {
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var playlistTrackPagingConverter = options.GetConverter<Paging<PlaylistTrack>>();
            var publicUserConverter = options.GetConverter<PublicUser>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("uri");
            uriConverter.Write(writer, value.Uri, options);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("name", value.Name);
            writer.WriteString("description", value.Description);
            writer.WritePropertyName("images");
            imageArrayConverter.Write(writer, value.Images, options);
            writer.WritePropertyName("owner");
            publicUserConverter.Write(writer, value.Owner, options);
            writer.WritePropertyName("tracks");
            playlistTrackPagingConverter.Write(writer, value.Tracks, options);
            if (value.IsPublic is Boolean isPublic)
            {
                writer.WriteBoolean("public", isPublic);
            }
            else
            {
                writer.WriteNull("public");
            }
            writer.WriteBoolean("collaborative", value.IsCollaborative);
            writer.WriteString("snapshot_id", value.SnapshotId);
            writer.WritePropertyName("external_urls");
            externalUrlsConverter.Write(writer, value.ExternalUrls, options);
            writer.WriteEndObject();
        }
    }
}
