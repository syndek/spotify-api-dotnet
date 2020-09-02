using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class PublicUserConverter : JsonConverter<PublicUser>
    {
        internal static readonly PublicUserConverter Instance = new();

        private PublicUserConverter() : base() { }

        public override PublicUser Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String? displayName = null;
            IReadOnlyList<Image> images = Array.Empty<Image>();
            Followers followers = Followers.None;
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
                    case "display_name":
                        displayName = reader.ReadString();
                        break;
                    case "images":
                        images = ArrayConverter<Image>.Instance.Read(ref reader, typeof(IReadOnlyList<Image>), options);
                        break;
                    case "followers":
                        followers = FollowersConverter.Instance.Read(ref reader, typeof(Followers), options);
                        break;
                    case "external_urls":
                        externalUrls = reader.ReadExternalUrls();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, uri, href, displayName, images, followers, externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, PublicUser value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}