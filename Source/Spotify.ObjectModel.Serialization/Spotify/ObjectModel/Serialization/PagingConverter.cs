using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class PagingConverter<TItem> : JsonConverter<Paging<TItem>>
    {
        public override Paging<TItem>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var itemArrayConverter = options.GetConverter<IReadOnlyList<TItem>>();
            var uriConverter = options.GetConverter<Uri>();

            IReadOnlyList<TItem> items = null!;
            Int32 total = default;
            Int32 limit = default;
            Int32 offset = default;
            Uri href = null!;
            Uri? previous = null;
            Uri? next = null;

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
                    case "items":
                        items = itemArrayConverter.Read(ref reader, typeof(IReadOnlyList<TItem>), options)!;
                        break;
                    case "total":
                        total = reader.GetInt32();
                        break;
                    case "limit":
                        limit = reader.GetInt32();
                        break;
                    case "offset":
                        offset = reader.GetInt32();
                        break;
                    case "href":
                        href = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "previous":
                        previous = (reader.TokenType is JsonTokenType.Null) ? null : uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "next":
                        next = (reader.TokenType is JsonTokenType.Null) ? null : uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(items, total, limit, offset, href, previous, next);
        }

        public override void Write(Utf8JsonWriter writer, Paging<TItem> value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}