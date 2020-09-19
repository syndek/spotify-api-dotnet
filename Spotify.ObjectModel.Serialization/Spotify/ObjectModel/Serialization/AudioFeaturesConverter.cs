using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class AudioFeaturesConverter : JsonConverter<AudioFeatures>
    {
        public static readonly AudioFeaturesConverter Instance = new();

        private AudioFeaturesConverter() : base() { }

        public override AudioFeatures Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri trackHref = null!;
            Uri analysisUrl = null!;
            Int32 duration = default;
            Int32 timeSignature = default;
            Int32 key = default;
            Int32 mode = default;
            Single acousticness = default;
            Single danceability = default;
            Single energy = default;
            Single instrumentalness = default;
            Single liveness = default;
            Single loudness = default;
            Single speechiness = default;
            Single tempo = default;
            Single valence = default;

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
                    case "track_href":
                        trackHref = reader.ReadUri();
                        break;
                    case "analysis_url":
                        analysisUrl = reader.ReadUri();
                        break;
                    case "duration":
                        duration = reader.ReadInt32();
                        break;
                    case "time_signature":
                        timeSignature = reader.ReadInt32();
                        break;
                    case "key":
                        key = reader.ReadInt32();
                        break;
                    case "mode":
                        mode = reader.ReadInt32();
                        break;
                    case "acousticness":
                        acousticness = reader.ReadSingle();
                        break;
                    case "danceability":
                        danceability = reader.ReadSingle();
                        break;
                    case "energy":
                        energy = reader.ReadSingle();
                        break;
                    case "instrumentalness":
                        instrumentalness = reader.ReadSingle();
                        break;
                    case "liveness":
                        liveness = reader.ReadSingle();
                        break;
                    case "loudness":
                        loudness = reader.ReadSingle();
                        break;
                    case "speechiness":
                        speechiness = reader.ReadSingle();
                        break;
                    case "tempo":
                        tempo = reader.ReadSingle();
                        break;
                    case "valence":
                        valence = reader.ReadSingle();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(
                id,
                uri,
                trackHref,
                analysisUrl,
                duration,
                timeSignature,
                key,
                mode,
                acousticness,
                danceability,
                energy,
                instrumentalness,
                liveness,
                loudness,
                speechiness,
                tempo,
                valence);
        }

        public override void Write(Utf8JsonWriter writer, AudioFeatures value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}