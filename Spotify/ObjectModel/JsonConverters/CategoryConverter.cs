using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class CategoryConverter : JsonConverter<Category>
    {
        internal static readonly CategoryConverter Instance = new();

        private CategoryConverter() : base() { }

        public override Category Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Category value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}