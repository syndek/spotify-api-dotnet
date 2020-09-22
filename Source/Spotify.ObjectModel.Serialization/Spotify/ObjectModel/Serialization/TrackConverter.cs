using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    using CountryCodeArray = IReadOnlyList<CountryCode>;
    using ExternalIds = IReadOnlyDictionary<String, String>;
    using ExternalUrls = IReadOnlyDictionary<String, Uri>;
    using SimplifiedArtistArray = IReadOnlyList<SimplifiedArtist>;

    public sealed class TrackConverter : JsonConverter<Track>
    {
        public override Track? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var countryCodeArrayConverter = options.GetConverter<CountryCodeArray>();
            var externalIdsConverter = options.GetConverter<ExternalIds>();
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var simplifiedAlbumConverter = options.GetConverter<SimplifiedAlbum>();
            var simplifiedArtistArrayConverter = options.GetConverter<SimplifiedArtistArray>();
            var uriConverter = options.GetConverter<Uri>();

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            SimplifiedAlbum album = null!;
            SimplifiedArtistArray artists = Array.Empty<SimplifiedArtist>();
            Int32 duration = default;
            Int32 trackNumber = default;
            Int32 discNumber = default;
            Boolean isExplicit = default;
            Boolean isLocal = default;
            CountryCodeArray availableMarkets = Array.Empty<CountryCode>();
            Int32 popularity = default;
            String previewUrl = String.Empty;
            ExternalIds externalIds = null!;
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
                    case "album":
                        album = simplifiedAlbumConverter.Read(ref reader, typeof(SimplifiedAlbum), options)!;
                        break;
                    case "artists":
                        artists = simplifiedArtistArrayConverter.Read(ref reader, typeof(SimplifiedArtistArray), options)!;
                        break;
                    case "duration_ms":
                        duration = reader.GetInt32();
                        break;
                    case "track_number":
                        trackNumber = reader.GetInt32();
                        break;
                    case "disc_number":
                        discNumber = reader.GetInt32();
                        break;
                    case "is_explicit":
                        isExplicit = reader.GetBoolean();
                        break;
                    case "is_local":
                        isLocal = reader.GetBoolean();
                        break;
                    case "available_markets":
                        availableMarkets = countryCodeArrayConverter.Read(ref reader, typeof(CountryCodeArray), options)!;
                        break;
                    case "popularity":
                        popularity = reader.GetInt32();
                        break;
                    case "preview_url":
                        previewUrl = reader.GetString()!;
                        break;
                    case "external_ids":
                        externalIds = externalIdsConverter.Read(ref reader, typeof(ExternalIds), options)!;
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
                album,
                artists,
                duration,
                discNumber,
                trackNumber,
                isExplicit,
                isLocal,
                availableMarkets,
                popularity,
                previewUrl,
                externalIds,
                externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, Track value, JsonSerializerOptions options)
        {
            var countryCodeArrayConverter = options.GetConverter<CountryCodeArray>();
            var externalIdsConverter = options.GetConverter<ExternalIds>();
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var simplifiedAlbumConverter = options.GetConverter<SimplifiedAlbum>();
            var simplifiedArtistArrayConverter = options.GetConverter<SimplifiedArtistArray>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("uri");
            uriConverter.Write(writer, value.Uri, options);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("name", value.Name);
            writer.WritePropertyName("album");
            simplifiedAlbumConverter.Write(writer, value.Album, options);
            writer.WritePropertyName("artists");
            simplifiedArtistArrayConverter.Write(writer, value.Artists, options);
            writer.WriteNumber("duration_ms", value.Duration);
            writer.WriteNumber("disc_number", value.DiscNumber);
            writer.WriteNumber("track_number", value.TrackNumber);
            writer.WriteBoolean("explicit", value.IsExplicit);
            writer.WriteBoolean("is_local", value.IsLocal);
            writer.WritePropertyName("available_markets");
            countryCodeArrayConverter.Write(writer, value.AvailableMarkets, options);
            writer.WriteNumber("popularity", value.Popularity);
            writer.WriteString("preview_url", value.PreviewUrl);
            writer.WritePropertyName("external_ids");
            externalIdsConverter.Write(writer, value.ExternalIds, options);
            writer.WritePropertyName("external_urls");
            externalUrlsConverter.Write(writer, value.ExternalUrls, options);
            writer.WriteEndObject();
        }
    }
}