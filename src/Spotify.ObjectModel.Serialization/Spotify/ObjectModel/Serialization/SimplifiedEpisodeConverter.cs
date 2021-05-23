using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    using ExternalUrls = IReadOnlyDictionary<string, Uri>;
    using ImageArray = IReadOnlyList<Image>;
    using StringArray = IReadOnlyList<string>;

    public sealed class SimplifiedEpisodeConverter : JsonConverter<SimplifiedEpisode>
    {
        public override SimplifiedEpisode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            string id = string.Empty;
            Uri uri = null!;
            Uri href = null!;
            string name = string.Empty;
            string description = string.Empty;
            ImageArray images = Array.Empty<Image>();
            int duration = default;
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            bool isExplicit = default;
            bool isPlayable = default;
            bool isExternallyHosted = default;
            StringArray languages = Array.Empty<string>();
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
                    case "duration_ms":
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

        public override void Write(Utf8JsonWriter writer, SimplifiedEpisode value, JsonSerializerOptions options)
        {
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var resumePointConverter = options.GetConverter<ResumePoint>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("type", "episode");
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("uri");
            uriConverter.Write(writer, value.Uri, options);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("name", value.Name);
            writer.WriteString("description", value.Description);
            writer.WritePropertyName("images");
            imageArrayConverter.Write(writer, value.Images, options);
            writer.WriteNumber("duration_ms", value.Duration);
            writer.WriteReleaseDate(value.ReleaseDate, value.ReleaseDatePrecision);
            writer.WriteString("release_date_precision", value.ReleaseDatePrecision.ToSpotifyString());
            writer.WriteBoolean("explicit", value.IsExplicit);
            writer.WriteBoolean("is_playable", value.IsPlayable);
            writer.WriteBoolean("is_externally_hosted", value.IsExternallyHosted);
            writer.WritePropertyName("languages");
            stringArrayConverter.Write(writer, value.Languages, options);

            if (value.AudioPreviewUrl is not null)
            {
                writer.WritePropertyName("audio_preview_url");
                uriConverter.Write(writer, value.AudioPreviewUrl, options);
            }
            else
            {
                writer.WriteNull("audio_preview_url");
            }

            writer.WritePropertyName("external_urls");
            externalUrlsConverter.Write(writer, value.ExternalUrls, options);

            if (value.ResumePoint is not null)
            {
                writer.WritePropertyName("resume_point");
                resumePointConverter.Write(writer, value.ResumePoint, options);
            }
            else
            {
                writer.WriteNull("resume_point");
            }

            writer.WriteEndObject();
        }
    }
}
