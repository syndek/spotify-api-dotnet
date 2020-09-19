using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class SimplifiedTrackConverter : JsonConverter<SimplifiedTrack>
    {
        public static readonly SimplifiedTrackConverter Instance = new();

        private SimplifiedTrackConverter() : base() { }

        public override SimplifiedTrack Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            IReadOnlyList<SimplifiedArtist> artists = Array.Empty<SimplifiedArtist>();
            Int32 duration = default;
            Int32 trackNumber = default;
            Int32 discNumber = default;
            Boolean isExplicit = default;
            Boolean isLocal = default;
            IReadOnlyList<CountryCode> availableMarkets = Array.Empty<CountryCode>();
            String previewUrl = String.Empty;
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
                    case "artists":
                        reader.Read(JsonTokenType.StartArray);
                        artists = reader.ReadArray<SimplifiedArtist>();
                        break;
                    case "duration":
                        duration = reader.ReadInt32();
                        break;
                    case "track_number":
                        trackNumber = reader.ReadInt32();
                        break;
                    case "disc_number":
                        discNumber = reader.ReadInt32();
                        break;
                    case "explicit":
                        isExplicit = reader.ReadBoolean();
                        break;
                    case "is_local":
                        isLocal = reader.ReadBoolean();
                        break;
                    case "available_markets":
                        reader.Read(JsonTokenType.StartArray);
                        availableMarkets = reader.ReadCountryCodeArray();
                        break;
                    case "preview_url":
                        previewUrl = reader.ReadString()!;
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
                artists,
                duration,
                discNumber,
                trackNumber,
                isExplicit,
                isLocal,
                availableMarkets,
                previewUrl,
                externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, SimplifiedTrack value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}