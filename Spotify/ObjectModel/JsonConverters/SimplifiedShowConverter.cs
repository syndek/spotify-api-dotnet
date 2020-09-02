using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class SimplifiedShowConverter : JsonConverter<SimplifiedShow>
    {
        internal static readonly SimplifiedShowConverter Instance = new();

        private SimplifiedShowConverter() : base() { }

        public override SimplifiedShow Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            String description = String.Empty;
            IReadOnlyList<Image> images = Array.Empty<Image>();
            Boolean isExplicit = default;
            IReadOnlyList<String> languages = Array.Empty<String>();
            IReadOnlyList<CountryCode> availableMarkets = Array.Empty<CountryCode>();
            String mediaType = String.Empty;
            String publisher = String.Empty;
            IReadOnlyList<Copyright> copyrights = Array.Empty<Copyright>();
            Boolean? isExternallyHosted = null;
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
                    case "description":
                        description = reader.ReadString()!;
                        break;
                    case "images":
                        reader.Read(JsonTokenType.StartArray);
                        images = ArrayConverter<Image>.Instance.Read(ref reader, typeof(IReadOnlyList<Image>), options);
                        break;
                    case "explicit":
                        isExplicit = reader.ReadBoolean();
                        break;
                    case "languages":
                        languages = reader.ReadStringArray();
                        break;
                    case "available_markets":
                        availableMarkets = reader.ReadCountryCodeArray();
                        break;
                    case "media_type":
                        mediaType = reader.ReadString()!;
                        break;
                    case "publisher":
                        publisher = reader.ReadString()!;
                        break;
                    case "copyrights":
                        reader.Read(JsonTokenType.StartArray);
                        copyrights = ArrayConverter<Copyright>.Instance.Read(ref reader, typeof(IReadOnlyList<Copyright>), options);
                        break;
                    case "is_externally_hosted":
                        reader.Read();
                        isExternallyHosted = reader.TokenType switch
                        {
                            JsonTokenType.Null => null,
                            JsonTokenType.True => true,
                            JsonTokenType.False => false,
                            _ => throw new JsonException("Invalid is_externally_hosted value.")
                        };
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
                description,
                images,
                isExplicit,
                isExternallyHosted,
                languages,
                availableMarkets,
                mediaType,
                publisher,
                copyrights,
                externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, SimplifiedShow value, JsonSerializerOptions options) => throw new NotImplementedException();
    }
}