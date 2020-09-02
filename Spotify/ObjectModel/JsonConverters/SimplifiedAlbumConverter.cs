using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.EnumConverters;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class SimplifiedAlbumConverter : JsonConverter<SimplifiedAlbum>
    {
        internal static readonly SimplifiedAlbumConverter Instance = new();

        private SimplifiedAlbumConverter() : base() { }

        public override SimplifiedAlbum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            AlbumType type = default;
            AlbumGroup? group = null;
            IReadOnlyList<Image> images = null!;
            IReadOnlyList<SimplifiedArtist> artists = null!;
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            IReadOnlyList<CountryCode> availableMarkets = null!;
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
                    case "album_type":
                        type = AlbumTypeConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    case "album_group":
                        group = AlbumGroupConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    case "images":
                        reader.Read(JsonTokenType.StartArray);
                        images = ArrayConverter<Image>.Instance.Read(ref reader, typeof(Image), options);
                        break;
                    case "artists":
                        reader.Read(JsonTokenType.StartArray);
                        artists = ArrayConverter<SimplifiedArtist>.Instance.Read(ref reader, typeof(SimplifiedArtist), options);
                        break;
                    case "release_date":
                        var date = reader.ReadString()!.Split('-');
                        releaseDate = new(
                            date.Length > 0 ? Int32.Parse(date[0]) : 1,
                            date.Length > 1 ? Int32.Parse(date[1]) : 1,
                            date.Length > 2 ? Int32.Parse(date[2]) : 1);
                        break;
                    case "release_date_precision":
                        releaseDatePrecision = ReleaseDatePrecisionConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    case "available_markets":
                        reader.Read(JsonTokenType.StartArray);
                        availableMarkets = reader.ReadCountryCodeArray();
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