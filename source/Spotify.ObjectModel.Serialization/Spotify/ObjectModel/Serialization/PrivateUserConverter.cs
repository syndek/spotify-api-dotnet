using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    using ExternalUrls = IReadOnlyDictionary<string, Uri>;
    using ImageArray = IReadOnlyList<Image>;

    public sealed class PrivateUserConverter : JsonConverter<PrivateUser>
    {
        public override PrivateUser Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var followersConverter = options.GetConverter<Followers>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var uriConverter = options.GetConverter<Uri>();

            string id = string.Empty;
            Uri uri = null!;
            Uri href = null!;
            string? email = null;
            string? displayName = null;
            CountryCode? country = null;
            ImageArray images = Array.Empty<Image>();
            Product? product = null;
            Followers followers = null!;
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
                        uri = uriConverter.Read(ref reader, typeof(ExternalUrls), options)!;
                        break;
                    case "href":
                        href = uriConverter.Read(ref reader, typeof(ExternalUrls), options)!;
                        break;
                    case "email":
                        email = reader.GetString();
                        break;
                    case "display_name":
                        displayName = reader.GetString();
                        break;
                    case "country":
                        var countryCode = reader.GetString();
                        country = countryCode is not null
                            ? EnumConverters.CountryCodeConverter.FromSpotifyString(countryCode)
                            : null;
                        break;
                    case "images":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    case "product":
                        product = ProductConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "followers":
                        followers = followersConverter.Read(ref reader, typeof(Followers), options)!;
                        break;
                    case "external_urls":
                        externalUrls = externalUrlsConverter.Read(ref reader, typeof(ExternalUrls), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, uri, href, email, displayName, country, images, product, followers, externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, PrivateUser value, JsonSerializerOptions options)
        {
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var followersConverter = options.GetConverter<Followers>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("type", "user");
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("uri");
            uriConverter.Write(writer, value.Uri, options);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("email", value.Email);
            writer.WriteString("display_name", value.DisplayName);
            writer.WriteString("country", value.Country?.ToSpotifyString());
            writer.WritePropertyName("images");
            imageArrayConverter.Write(writer, value.Images, options);
            writer.WriteString("product", value.Product?.ToSpotifyString());
            writer.WritePropertyName("followers");
            followersConverter.Write(writer, value.Followers, options);
            writer.WritePropertyName("external_urls");
            externalUrlsConverter.Write(writer, value.ExternalUrls, options);
            writer.WriteEndObject();
        }
    }
}
