using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class PlayableConverter : JsonConverter<IPlayable>
    {
        public override IPlayable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // A IPlayable can be one of 2 types: 'Episode' or 'Track'.
            // If somebody tries to use this converter for a different type that implements IPlayable, that's their problem, not ours.

            // Most Spotify objects contain a 'type' property that specify the type of object represented by the data.
            // However, this property is not necessarily at the beginning of the object, so we have to read the JSON into a JsonDocument first.

            // We don't want to operate on the original reader, so we make a copy of it to parse as the JsonDocument.
            var typeReader = reader;

            if (JsonDocument.ParseValue(ref typeReader).RootElement.TryGetProperty("type", out var value))
            {
                switch (value.GetString())
                {
                    case "episode":
                        return options.GetConverter<Episode>().Read(ref reader, typeof(Episode), options);
                    case "track":
                        return options.GetConverter<Track>().Read(ref reader, typeof(Track), options);
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IPlayable value, JsonSerializerOptions options)
        {
            // No need to examine the IPlayable object here.
            // Simply get a converter for whatever the underlying type is and use that.

            var playableConverter = (JsonConverter<IPlayable>) options.GetConverter(value.GetType());
            playableConverter.Write(writer, value, options);
        }
    }
}