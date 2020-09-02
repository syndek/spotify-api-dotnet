using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class CurrentlyPlayingConverter : JsonConverter<CurrentlyPlaying>
    {
        internal static readonly CurrentlyPlayingConverter Instance = new();

        private CurrentlyPlayingConverter() : base() { }

        public override CurrentlyPlaying Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, CurrentlyPlaying value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}