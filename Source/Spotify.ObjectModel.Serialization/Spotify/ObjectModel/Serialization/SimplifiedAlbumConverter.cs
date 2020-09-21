using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    using CountryCodeArray = IReadOnlyList<CountryCode>;
    using ExternalUrls = IReadOnlyDictionary<String, Uri>;
    using ImageArray = IReadOnlyList<Image>;
    using SimplifiedArtistArray = IReadOnlyList<SimplifiedArtist>;

    public sealed class SimplifiedAlbumConverter : JsonConverter<SimplifiedAlbum>
    {
        public override SimplifiedAlbum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var countryCodeArrayConverter = options.GetConverter<CountryCodeArray>();
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var simplifiedArtistArrayConverter = options.GetConverter<SimplifiedArtistArray>();
            var uriConverter = options.GetConverter<Uri>();

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            AlbumType type = default;
            AlbumGroups? group = null;
            ImageArray images = null!;
            SimplifiedArtistArray artists = null!;
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            CountryCodeArray availableMarkets = null!;
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
                    case "album_type":
                        type = AlbumTypeConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "album_group":
                        group = AlbumGroupConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "images":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    case "artists":
                        artists = simplifiedArtistArrayConverter.Read(ref reader, typeof(SimplifiedArtistArray), options)!;
                        break;
                    case "release_date":
                        var date = reader.GetString()!.Split('-');
                        releaseDate = new(
                            date.Length > 0 ? Int32.Parse(date[0]) : 1,
                            date.Length > 1 ? Int32.Parse(date[1]) : 1,
                            date.Length > 2 ? Int32.Parse(date[2]) : 1);
                        break;
                    case "release_date_precision":
                        releaseDatePrecision = ReleaseDatePrecisionConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "available_markets":
                        availableMarkets = countryCodeArrayConverter.Read(ref reader, typeof(CountryCodeArray), options)!;
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
                group,
                images,
                artists,
                releaseDate,
                releaseDatePrecision,
                availableMarkets,
                externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, SimplifiedAlbum value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}