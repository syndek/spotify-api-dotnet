using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.EnumConverters;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class AlbumConverter : JsonConverter<Album>
    {
        internal static readonly AlbumConverter Instance = new();

        private AlbumConverter() : base() { }

        public override Album Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            AlbumType type = default;
            IReadOnlyList<Image> images = Array.Empty<Image>();
            IReadOnlyList<SimplifiedArtist> artists = Array.Empty<SimplifiedArtist>();
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            IReadOnlyList<CountryCode> availableMarkets = Array.Empty<CountryCode>();
            IReadOnlyList<Copyright> copyrights = Array.Empty<Copyright>();
            IReadOnlyDictionary<String, String> externalIds = null!;
            IReadOnlyDictionary<String, Uri> externalUrls = null!;
            IReadOnlyList<String> genres = Array.Empty<String>();
            String label = String.Empty;
            Int32 popularity = default;
            Paging<SimplifiedTrack> tracks = Paging<SimplifiedTrack>.Empty;

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
                    case "images":
                        reader.Read(JsonTokenType.StartArray);
                        images = reader.ReadArray<Image>();
                        break;
                    case "artists":
                        reader.Read(JsonTokenType.StartArray);
                        artists = reader.ReadArray<SimplifiedArtist>();
                        break;
                    case "release_date":
                        releaseDate = reader.ReadDateTime();
                        break;
                    case "release_date_precision":
                        releaseDatePrecision = ReleaseDatePrecisionConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    case "tracks":
                        reader.Read(JsonTokenType.StartArray);
                        tracks = PagingConverter<SimplifiedTrack>.Instance.Read(ref reader, typeof(Paging<SimplifiedTrack>), options);
                        break;
                    case "genres":
                        reader.Read(JsonTokenType.StartArray);
                        genres = reader.ReadArray<String>();
                        break;
                    case "popularity":
                        popularity = reader.ReadInt32();
                        break;
                    case "available_markets":
                        reader.Read(JsonTokenType.StartArray);
                        availableMarkets = reader.ReadCountryCodeArray();
                        break;
                    case "label":
                        label = reader.ReadString()!;
                        break;
                    case "copyrights":
                        reader.Read(JsonTokenType.StartArray);
                        copyrights = reader.ReadArray<Copyright>();
                        break;
                    case "external_ids":
                        reader.Read(JsonTokenType.StartObject);
                        externalIds = reader.ReadStringDictionary();
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

        public override void Write(Utf8JsonWriter writer, Album value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}