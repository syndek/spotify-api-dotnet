using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class PlaylistTrackConverter : JsonConverter<PlaylistTrack>
    {
        public override PlaylistTrack Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var playableConverter = options.GetConverter<IPlayable>();
            var publicUserConverter = options.GetConverter<PublicUser>();

            DateTime addedAt = default;
            PublicUser addedBy = null!;
            bool isLocal = default;
            IPlayable track = null!;

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
                    case "added_at":
                        addedAt = reader.GetDateTime();
                        break;
                    case "added_by":
                        addedBy = publicUserConverter.Read(ref reader, typeof(PublicUser), options)!;
                        break;
                    case "is_local":
                        isLocal = reader.GetBoolean();
                        break;
                    case "track":
                        track = playableConverter.Read(ref reader, typeof(IPlayable), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(addedAt, addedBy, isLocal, track);
        }

        public override void Write(Utf8JsonWriter writer, PlaylistTrack value, JsonSerializerOptions options)
        {
            var playableConverter = options.GetConverter<IPlayable>();
            var publicUserConverter = options.GetConverter<PublicUser>();

            writer.WriteStartObject();
            writer.WriteString("added_at", value.AddedAt);
            writer.WritePropertyName("added_by");
            publicUserConverter.Write(writer, value.AddedBy, options);
            writer.WriteBoolean("is_local", value.IsLocal);
            writer.WritePropertyName("track");
            playableConverter.Write(writer, value.Track, options);
            writer.WriteEndObject();
        }
    }
}
