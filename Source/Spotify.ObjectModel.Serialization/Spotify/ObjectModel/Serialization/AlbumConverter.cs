using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    using CopyrightArray = IReadOnlyList<Copyright>;
    using CountryCodeArray = IReadOnlyList<CountryCode>;
    using ExternalIds = IReadOnlyDictionary<String, String>;
    using ExternalUrls = IReadOnlyDictionary<String, Uri>;
    using ImageArray = IReadOnlyList<Image>;
    using SimplifiedArtistArray = IReadOnlyList<SimplifiedArtist>;
    using StringArray = IReadOnlyList<String>;

    public sealed class AlbumConverter : JsonConverter<Album>
    {
        public override Album? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var copyrightArrayConverter = options.GetConverter<CopyrightArray>();
            var countryCodeArrayConverter = options.GetConverter<CountryCodeArray>();
            var externalIdsConverter = options.GetConverter<ExternalIds>();
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var simplifiedArtistArrayConverter = options.GetConverter<SimplifiedArtistArray>();
            var simplifiedTrackPagingConverter = options.GetConverter<Paging<SimplifiedTrack>>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            AlbumType type = default;
            ImageArray images = Array.Empty<Image>();
            SimplifiedArtistArray artists = Array.Empty<SimplifiedArtist>();
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            CountryCodeArray availableMarkets = Array.Empty<CountryCode>();
            CopyrightArray copyrights = Array.Empty<Copyright>();
            ExternalIds externalIds = null!;
            ExternalUrls externalUrls = null!;
            StringArray genres = Array.Empty<String>();
            String label = String.Empty;
            Int32 popularity = default;
            Paging<SimplifiedTrack> tracks = Paging<SimplifiedTrack>.Empty;

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
                    case "album_type":
                        type = AlbumTypeConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "images":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    case "artists":
                        artists = simplifiedArtistArrayConverter.Read(ref reader, typeof(SimplifiedArtistArray), options)!;
                        break;
                    case "release_date":
                        releaseDate = reader.GetReleaseDate();
                        break;
                    case "release_date_precision":
                        releaseDatePrecision = ReleaseDatePrecisionConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "tracks":
                        tracks = simplifiedTrackPagingConverter.Read(ref reader, typeof(Paging<SimplifiedTrack>), options)!;
                        break;
                    case "genres":
                        genres = stringArrayConverter.Read(ref reader, typeof(StringArray), options)!;
                        break;
                    case "popularity":
                        popularity = reader.GetInt32();
                        break;
                    case "available_markets":
                        availableMarkets = countryCodeArrayConverter.Read(ref reader, typeof(CountryCodeArray), options)!;
                        break;
                    case "label":
                        label = reader.GetString()!;
                        break;
                    case "copyrights":
                        copyrights = copyrightArrayConverter.Read(ref reader, typeof(CopyrightArray), options)!;
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
                type,
                images,
                artists,
                releaseDate,
                releaseDatePrecision,
                tracks,
                genres,
                popularity,
                availableMarkets,
                label,
                copyrights,
                externalIds,
                externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, Album value, JsonSerializerOptions options)
        {
            var copyrightArrayConverter = options.GetConverter<CopyrightArray>();
            var countryCodeArrayConverter = options.GetConverter<CountryCodeArray>();
            var externalIdsConverter = options.GetConverter<ExternalIds>();
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var simplifiedArtistArrayConverter = options.GetConverter<SimplifiedArtistArray>();
            var simplifiedTrackPagingConverter = options.GetConverter<Paging<SimplifiedTrack>>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("uri");
            uriConverter.Write(writer, value.Uri, options);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("name", value.Name);
            writer.WriteString("type", value.Type.ToSpotifyString());
            writer.WritePropertyName("images");
            imageArrayConverter.Write(writer, value.Images, options);
            writer.WritePropertyName("artists");
            simplifiedArtistArrayConverter.Write(writer, value.Artists, options);
            writer.WriteReleaseDate(value.ReleaseDate, value.ReleaseDatePrecision);
            writer.WriteString("release_date_precision", value.ReleaseDatePrecision.ToSpotifyString());
            writer.WritePropertyName("tracks");
            simplifiedTrackPagingConverter.Write(writer, value.Tracks, options);
            writer.WritePropertyName("genres");
            stringArrayConverter.Write(writer, value.Genres, options);
            writer.WriteNumber("popularity", value.Popularity);
            writer.WritePropertyName("available_markets");
            countryCodeArrayConverter.Write(writer, value.AvailableMarkets, options);
            writer.WriteString("label", value.Label);
            writer.WritePropertyName("copyrights");
            copyrightArrayConverter.Write(writer, value.Copyrights, options);
            writer.WritePropertyName("external_ids");
            externalIdsConverter.Write(writer, value.ExternalIds, options);
            writer.WritePropertyName("external_urls");
            externalUrlsConverter.Write(writer, value.ExternalUrls, options);
            writer.WriteEndObject();
        }
    }
}