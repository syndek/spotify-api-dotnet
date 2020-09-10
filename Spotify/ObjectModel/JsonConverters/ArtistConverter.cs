using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class ArtistConverter : JsonConverter<Artist>
    {
        internal static readonly ArtistConverter Instance = new();

        private ArtistConverter() : base() { }

        public override Artist Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            IReadOnlyList<Image> images = Array.Empty<Image>();
            Followers followers = Followers.None;
            IReadOnlyList<String> genres = Array.Empty<String>();
            Int32 popularity = default;
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
                    case "images":
                        reader.Read(JsonTokenType.StartArray);
                        images = reader.ReadArray<Image>();
                        break;
                    case "followers":
                        reader.Read(JsonTokenType.StartObject);
                        followers = FollowersConverter.Instance.Read(ref reader, typeof(Followers), options);
                        break;
                    case "genres":
                        reader.Read(JsonTokenType.StartArray);
                        genres = reader.ReadStringArray();
                        break;
                    case "popularity":
                        popularity = reader.ReadInt32();
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

            return new(id, uri, href, name, images, followers, genres, popularity, externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, Artist value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}