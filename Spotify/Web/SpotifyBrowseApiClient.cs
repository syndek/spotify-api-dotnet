using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.EnumConverters;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for getting recommendations,
    /// and playlists/new album releases featured on Spotify’s Browse tab.
    /// </summary>
    public class SpotifyBrowseApiClient : SpotifyApiClient, ISpotifyBrowseApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyBrowseApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyBrowseApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc/>
        public Task<Paging<Category>> GetCategoriesAsync(
            CountryCode? country = null,
            String? locale = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/browse/categories")
                .AppendToQueryIfNotNull("country", country?.ToSpotifyString())
                .AppendToQueryIfNotNull("locale", locale)
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<Category>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Category> GetCategoryAsync(
            String id,
            CountryCode? country = null,
            String? locale = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/browse/categories/{id}")
                .AppendToQueryIfNotNull("country", country?.ToSpotifyString())
                .AppendToQueryIfNotNull("locale", locale);

            return base.SendAsync<Category>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<SimplifiedPlaylist>> GetCategoryPlaylistsAsync(
            String id,
            CountryCode? country = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/browse/categories/{id}/playlists")
                .AppendToQueryIfNotNull("country", country?.ToSpotifyString())
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<SimplifiedPlaylist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Recommendations> GetRecommendationsAsync(
            IReadOnlyList<String> seedArtists,
            IReadOnlyList<String> seedTracks,
            IReadOnlyList<String> seedGenres,
            Int32? limit = null,
            CountryCode? market = null,
            TuneableTrackAttributes? minValues = null,
            TuneableTrackAttributes? maxValues = null,
            TuneableTrackAttributes? targetValues = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            static IEnumerable<(String, ValueType?)> GetAttributeQueryStrings(String prefix, TuneableTrackAttributes attributes)
            {
                yield return ($"{prefix}_popularity", attributes.Popularity);
                yield return ($"{prefix}_duration", attributes.Duration);
                yield return ($"{prefix}_time_signature", attributes.TimeSignature);
                yield return ($"{prefix}_key", attributes.Key);
                yield return ($"{prefix}_mode", attributes.Mode);
                yield return ($"{prefix}_acousticness", attributes.Acousticness);
                yield return ($"{prefix}_danceability", attributes.Danceability);
                yield return ($"{prefix}_energy", attributes.Energy);
                yield return ($"{prefix}_instrumentalness", attributes.Instrumentalness);
                yield return ($"{prefix}_liveness", attributes.Liveness);
                yield return ($"{prefix}_loudness", attributes.Loudness);
                yield return ($"{prefix}_speechiness", attributes.Speechiness);
                yield return ($"{prefix}_tempo", attributes.Tempo);
                yield return ($"{prefix}_valence", attributes.Valence);
            }

            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/recommendations")
                .AppendJoinToQuery("seed_artists", ',', seedArtists)
                .AppendJoinToQuery("seed_tracks", ',', seedTracks)
                .AppendJoinToQuery("seed_genres", ',', seedGenres)
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("market", market);

            void AppendAttributesToUriBuilderIfNotNull(String prefix, TuneableTrackAttributes? attributes)
            {
                if (attributes is not null)
                {
                    foreach (var (name, attribute) in GetAttributeQueryStrings(prefix, attributes))
                    {
                        uriBuilder.AppendToQueryIfNotNull(name, attribute);
                    }
                }
            }

            AppendAttributesToUriBuilderIfNotNull("min", minValues);
            AppendAttributesToUriBuilderIfNotNull("max", maxValues);
            AppendAttributesToUriBuilderIfNotNull("target", targetValues);

            return base.SendAsync<Recommendations>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<String>> GetRecommendationGenresAsync(
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<IReadOnlyList<String>>(
                new($"{SpotifyApiClient.BaseUrl}/recommendations/available-genre-seeds"),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<SimplifiedPlaylist>> GetFeaturedPlaylistsAsync(
            String? locale = null,
            CountryCode? country = null,
            DateTime? timestamp = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/browse/featured-playlists")
                .AppendToQueryIfNotNull("locale", locale)
                .AppendToQueryIfNotNull("country", country?.ToSpotifyString())
                .AppendToQueryIfNotNull("timestamp", timestamp)
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<SimplifiedPlaylist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<SimplifiedAlbum>> GetNewReleasesAsync(
            CountryCode? country = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/browse/new-releases")
                .AppendToQueryIfNotNull("country", country?.ToSpotifyString())
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<SimplifiedAlbum>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }
    }
}