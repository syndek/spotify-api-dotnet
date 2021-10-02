using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    using ExternalUrls = IReadOnlyDictionary<string, Uri>;
    using ImageArray = IReadOnlyList<Image>;
    using StringArray = IReadOnlyList<string>;

    public sealed class ArtistConverter : JsonConverter<Artist>
    {
        public override Artist Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var followersConverter = options.GetConverter<Followers>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            string id = string.Empty;
            Uri uri = null!;
            Uri href = null!;
            string name = string.Empty;
            ImageArray images = Array.Empty<Image>();
            Followers followers = null!;
            StringArray genres = Array.Empty<string>();
            int popularity = default;
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
                    case "images":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    case "followers":
                        followers = followersConverter.Read(ref reader, typeof(Followers), options)!;
                        break;
                    case "genres":
                        genres = stringArrayConverter.Read(ref reader, typeof(StringArray), options)!;
                        break;
                    case "popularity":
                        popularity = reader.GetInt32();
                        break;
                    case "external_urls":
                        externalUrls = externalUrlsConverter.Read(ref reader, typeof(ExternalUrls), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, uri, href, name, images, followers, genres, popularity, externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, Artist value, JsonSerializerOptions options)
        {
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var followersConverter = options.GetConverter<Followers>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("type", "artist");
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("uri");
            uriConverter.Write(writer, value.Uri, options);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("name", value.Name);
            writer.WritePropertyName("images");
            imageArrayConverter.Write(writer, value.Images, options);
            writer.WritePropertyName("followers");
            followersConverter.Write(writer, value.Followers, options);
            writer.WritePropertyName("genres");
            stringArrayConverter.Write(writer, value.Genres, options);
            writer.WriteNumber("popularity", value.Popularity);
            writer.WritePropertyName("external_urls");
            externalUrlsConverter.Write(writer, value.ExternalUrls, options);
            writer.WriteEndObject();
        }
    }
}
