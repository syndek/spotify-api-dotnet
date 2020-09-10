using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class PagingConverter<TItem> : JsonConverter<Paging<TItem>>
    {
        internal static readonly PagingConverter<TItem> Instance = new();

        private PagingConverter() : base() { }

        public override Paging<TItem> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            IReadOnlyList<TItem> items = null!;
            Int32 total = default;
            Int32 limit = default;
            Int32 offset = default;
            Uri href = null!;
            Uri? previous = null;
            Uri? next = null;

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
                    case "items":
                        reader.Read(JsonTokenType.StartArray);
                        items = reader.ReadArray<TItem>();
                        break;
                    case "total":
                        total = reader.ReadInt32();
                        break;
                    case "limit":
                        limit = reader.ReadInt32();
                        break;
                    case "offset":
                        offset = reader.ReadInt32();
                        break;
                    case "href":
                        href = reader.ReadUri();
                        break;
                    case "previous":
                        previous = reader.ReadNullableUri();
                        break;
                    case "next":
                        next = reader.ReadNullableUri();
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