using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class SimplifiedArtistConverter : JsonConverter<SimplifiedArtist>
    {
        internal static readonly SimplifiedArtistConverter Instance = new();

        private SimplifiedArtistConverter() : base() { }

        public override SimplifiedArtist Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
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
                    case "external_urls":
                        reader.Read(JsonTokenType.StartObject);
                        externalUrls = reader.ReadExternalUrls();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(id, uri, href, name, externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, SimplifiedArtist value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}