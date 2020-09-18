using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.EnumConverters;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class PrivateUserConverter : JsonConverter<PrivateUser>
    {
        internal static readonly PrivateUserConverter Instance = new();

        private PrivateUserConverter() : base() { }

        public override PrivateUser Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String? email = null;
            String? displayName = null;
            CountryCode? country = null;
            IReadOnlyList<Image> images = Array.Empty<Image>();
            Product? product = null;
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
                    case "email":
                        email = reader.ReadString();
                        break;
                    case "display_name":
                        displayName = reader.ReadString();
                        break;
                    case "country":
                        country = CountryCodeConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    case "images":
                        reader.Read(JsonTokenType.StartArray);
                        images = reader.ReadArray<Image>();
                        break;
                    case "product":
                        product = ProductConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    case "followers":
                        reader.Read(JsonTokenType.StartObject);
                        followers = FollowersConverter.Instance.Read(ref reader, typeof(Followers), options);
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

            return new(id, uri, href, email, displayName, country, images, product, followers, externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, PrivateUser value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}