using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.EnumConverters;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class SimplifiedEpisodeConverter : JsonConverter<SimplifiedEpisode>
    {
        internal static readonly SimplifiedEpisodeConverter Instance = new();

        private SimplifiedEpisodeConverter() : base() { }

        public override SimplifiedEpisode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            String description = String.Empty;
            IReadOnlyList<Image> images = Array.Empty<Image>();
            Int32 duration = default;
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            Boolean isExplicit = default;
            Boolean isPlayable = default;
            Boolean isExternallyHosted = default;
            IReadOnlyList<String> languages = Array.Empty<String>();
            Uri? audioPreviewUrl = null;
            IReadOnlyDictionary<String, Uri> externalUrls = null!;
            ResumePoint? resumePoint = null;

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
                    case "href":
                        href = reader.ReadUri();
                        break;
                    case "name":
                        name = reader.ReadString()!;
                        break;
                    case "description":
                        description = reader.ReadString()!;
                        break;
                    case "images":
                        reader.Read(JsonTokenType.StartArray);
                        images = ArrayConverter<Image>.Instance.Read(ref reader, typeof(IReadOnlyList<Image>), options);
                        break;
                    case "duration":
                        duration = reader.ReadInt32();
                        break;
                    case "release_date":
                        releaseDate = reader.ReadDateTime();
                        break;
                    case "release_date_precision":
                        releaseDatePrecision = ReleaseDatePrecisionConverter.FromSpotifyString(reader.ReadString()!);
                        break;
                    case "explicit":
                        isExplicit = reader.ReadBoolean();
                        break;
                    case "is_playable":
                        isPlayable = reader.ReadBoolean();
                        break;
                    case "is_externally_hosted":
                        isExternallyHosted = reader.ReadBoolean();
                        break;
                    case "languages":
                        reader.Read(JsonTokenType.StartArray);
                        languages = reader.ReadStringArray();
                        break;
                    case "audio_preview_url":
                        audioPreviewUrl = reader.ReadNullableUri();
                        break;
                    case "external_urls":
                        reader.Read(JsonTokenType.StartObject);
                        externalUrls = reader.ReadExternalUrls();
                        break;
                    case "resume_point":
                        reader.Read(JsonTokenType.StartObject);
                        resumePoint = ResumePointConverter.Instance.Read(ref reader, typeof(ResumePoint), options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(
                id,
                uri,
                href,
                name,
                description,
                images,
                duration,
                releaseDate,
                releaseDatePrecision,
                isExplicit,
                isPlayable,
                isExternallyHosted,
                languages,
                audioPreviewUrl,
                externalUrls,
                resumePoint);
        }

        public override void Write(Utf8JsonWriter writer, SimplifiedEpisode value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}