using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    using ExternalUrls = IReadOnlyDictionary<String, Uri>;
    using ImageArray = IReadOnlyList<Image>;
    using StringArray = IReadOnlyList<String>;

    public sealed class SimplifiedEpisodeConverter : JsonConverter<SimplifiedEpisode>
    {
        public override SimplifiedEpisode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var resumePointConverter = options.GetConverter<ResumePoint>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            String description = String.Empty;
            ImageArray images = Array.Empty<Image>();
            Int32 duration = default;
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            Boolean isExplicit = default;
            Boolean isPlayable = default;
            Boolean isExternallyHosted = default;
            StringArray languages = Array.Empty<String>();
            Uri? audioPreviewUrl = null;
            ExternalUrls externalUrls = null!;
            ResumePoint? resumePoint = null;

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
                    case "id":
                        id = reader.GetString()!;
                        break;
                    case "uri":
                        uri = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "href":
                        href = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "name":
                        name = reader.GetString()!;
                        break;
                    case "description":
                        description = reader.GetString()!;
                        break;
                    case "images":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    case "duration":
                        duration = reader.GetInt32();
                        break;
                    case "release_date":
                        releaseDate = reader.GetReleaseDate();
                        break;
                    case "release_date_precision":
                        releaseDatePrecision = ReleaseDatePrecisionConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "explicit":
                        isExplicit = reader.GetBoolean();
                        break;
                    case "is_playable":
                        isPlayable = reader.GetBoolean();
                        break;
                    case "is_externally_hosted":
                        isExternallyHosted = reader.GetBoolean();
                        break;
                    case "languages":
                        languages = stringArrayConverter.Read(ref reader, typeof(StringArray), options)!;
                        break;
                    case "audio_preview_url":
                        audioPreviewUrl = (reader.TokenType is JsonTokenType.Null) ? null : uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "external_urls":
                        externalUrls = externalUrlsConverter.Read(ref reader, typeof(ExternalUrls), options)!;
                        break;
                    case "resume_point":
                        resumePoint = resumePointConverter.Read(ref reader, typeof(ResumePoint), options)!;
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