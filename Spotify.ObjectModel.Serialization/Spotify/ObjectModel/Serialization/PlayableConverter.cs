using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class PlayableConverter : JsonConverter<IPlayable>
    {
        public override IPlayable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IPlayable value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}