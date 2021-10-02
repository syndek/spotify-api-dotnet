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

            if (!JsonDocument.ParseValue(ref typeReader).RootElement.TryGetProperty("type", out var value))
            {
                throw new JsonException();
            }

            return value.GetString() switch
            {
                "episode" => options.GetConverter<Episode>().Read(ref reader, typeof(Episode), options),
                "track" => options.GetConverter<Track>().Read(ref reader, typeof(Track), options),
                _ => throw new JsonException()
            };
        }

        public override void Write(Utf8JsonWriter writer, IPlayable value, JsonSerializerOptions options)
        {
            // Like with Read(ref Utf8JsonReader, Type, JsonSerializerOptions), if somebody attempts to use this converter
            // with an IPlayable implementation that is not recognised by this library, there's nothing we can do to help them.
            // (Custom IPlayable implementations would technically be unintended use of the library types, anyway. Sealed interfaces when?)

            switch (value)
            {
                case Episode episode:
                    options.GetConverter<Episode>().Write(writer, episode, options);
                    break;
                case Track track:
                    options.GetConverter<Track>().Write(writer, track, options);
                    break;
                default:
                    throw new JsonException();
            }
        }
    }
}
