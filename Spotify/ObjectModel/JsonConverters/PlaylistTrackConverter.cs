using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class PlaylistTrackConverter : JsonConverter<PlaylistTrack>
    {
        internal static readonly PlaylistTrackConverter Instance = new();

        private PlaylistTrackConverter() : base() { }

        public override PlaylistTrack Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            DateTime addedAt = default;
            PublicUser addedBy = null!;
            Boolean isLocal = default;
            IPlayable track = null!;

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
                    case "added_at":
                        addedAt = reader.ReadDateTime();
                        break;
                    case "added_by":
                        reader.Read(JsonTokenType.StartObject);
                        addedBy = PublicUserConverter.Instance.Read(ref reader, typeof(PublicUser), options);
                        break;
                    case "is_local":
                        isLocal = reader.ReadBoolean();
                        break;
                    case "track":
                        reader.Read(JsonTokenType.StartObject);
                        track = PlayableConverter.Instance.Read(ref reader, typeof(IPlayable), options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(addedAt, addedBy, isLocal, track);
        }

        public override void Write(Utf8JsonWriter writer, PlaylistTrack value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}