using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.EnumConverters;

namespace Spotify.ObjectModel.JsonConverters
{
    /// <summary>
    /// Provides extensions methods and utilities for use with instances of <see cref="JsonConverter{T}"/>.
    /// </summary>
    internal static class ConverterHelpers : Object
    {
        internal static readonly JsonSerializerOptions JsonSerializerOptions = new();

        static ConverterHelpers()
        {
            var converters = ConverterHelpers.JsonSerializerOptions.Converters;

            // Add converters for ObjectModel types.
            converters.Add(AlbumConverter.Instance);
            converters.Add(ArtistConverter.Instance);
            converters.Add(AudioAnalysisConverter.Instance);
            converters.Add(AudioFeaturesConverter.Instance);
            converters.Add(CategoryConverter.Instance);
            converters.Add(ContextConverter.Instance);
            converters.Add(CopyrightConverter.Instance);
            converters.Add(EpisodeConverter.Instance);
            converters.Add(FollowersConverter.Instance);
            converters.Add(ImageConverter.Instance);
            converters.Add(PrivateUserConverter.Instance);
            converters.Add(PublicUserConverter.Instance);
            converters.Add(RecommendationsConverter.Instance);
            converters.Add(RecommendationSeedConverter.Instance);
            converters.Add(SearchResultConverter.Instance);
            converters.Add(SectionConverter.Instance);
            converters.Add(SegmentConverter.Instance);
            converters.Add(ShowConverter.Instance);
            converters.Add(SimplifiedAlbumConverter.Instance);
            converters.Add(SimplifiedArtistConverter.Instance);
            converters.Add(SimplifiedEpisodeConverter.Instance);
            converters.Add(SimplifiedShowConverter.Instance);
            converters.Add(SimplifiedTrackConverter.Instance);
            converters.Add(TimeIntervalConverter.Instance);
            converters.Add(TrackConverter.Instance);

            // Add converters for custom types belonging to the library.
            converters.Add(ErrorConverter.Instance);
            converters.Add(AuthenticationErrorConverter.Instance);
            converters.Add(AccessTokenConverter.Instance);
            converters.Add(AccessRefreshTokenConverter.Instance);
            converters.Add(ArrayConverter<Track>.Instance);
            converters.Add(NamedArrayConverter<String>.Instance);
            converters.Add(NullableArrayConverter<Album>.Instance);
            converters.Add(PagingConverter<SimplifiedTrack>.Instance);
            converters.Add(PagingConverter<Artist>.Instance);
            converters.Add(PagingConverter<Track>.Instance);
        }

        internal static Uri ReadUri(ref this Utf8JsonReader reader)
        {
            reader.Read(JsonTokenType.String);
            return new(reader.GetString()!);
        }

        internal static Uri? ReadNullableUri(ref this Utf8JsonReader reader)
        {
            reader.Read();

            if ((reader.TokenType != JsonTokenType.String) && (reader.TokenType != JsonTokenType.Null))
            {
                throw new JsonException();
            }

            var uriString = reader.GetString();
            if (uriString is not null)
            {
                return new(uriString);
            }

            return null;
        }

        internal static IReadOnlyList<TObject> ReadArray<TObject>(ref this Utf8JsonReader reader) =>
            ArrayConverter<TObject>.Instance.Read(ref reader, typeof(IReadOnlyList<TObject>), ConverterHelpers.JsonSerializerOptions);

        internal static Paging<TObject> ReadPaging<TObject>(ref this Utf8JsonReader reader) =>
            PagingConverter<TObject>.Instance.Read(ref reader, typeof(Paging<TObject>), ConverterHelpers.JsonSerializerOptions);

        internal static IReadOnlyDictionary<String, String> ReadStringDictionary(ref this Utf8JsonReader reader)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            var dictionary = new Dictionary<String, String>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                dictionary.Add(reader.GetString()!, reader.ReadString()!);
            }

            throw new JsonException();
        }

        internal static IReadOnlyDictionary<String, Uri> ReadExternalUrls(ref this Utf8JsonReader reader)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            var dictionary = new Dictionary<String, Uri>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                dictionary.Add(reader.GetString()!, reader.ReadUri());
            }

            throw new JsonException();
        }

        internal static IReadOnlyList<CountryCode> ReadCountryCodeArray(ref this Utf8JsonReader reader)
        {
            reader.AssertTokenType(JsonTokenType.StartArray);

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var countryCodes = new List<CountryCode>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return countryCodes.AsReadOnly();
                }

                countryCodes.Add(CountryCodeConverter.FromSpotifyString(reader.GetString()!));
            }

            throw new JsonException();
        }
    }
}