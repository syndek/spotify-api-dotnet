using System;
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/browse/categories")
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/browse/categories/{id}")
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/browse/categories/{id}/playlists")
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
            Int32? limit = null,
            CountryCode? market = null,
            TuneableTrackAttributes? minValues = null,
            TuneableTrackAttributes? maxValues = null,
            TuneableTrackAttributes? targetValues = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<GenreSeedList> GetRecommendationGenresAsync(
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<GenreSeedList>(
                new($"{SpotifyApiClient.BaseUri}/recommendations/available-genre-seeds"),
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/browse/featured-playlists")
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/browse/new-releases")
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