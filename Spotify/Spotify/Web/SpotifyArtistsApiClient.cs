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
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more artists from the Spotify catalog.
    /// </summary>
    public class SpotifyArtistsApiClient : SpotifyApiClient, ISpotifyArtistsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyArtistsApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyArtistsApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Artist>> GetArtistsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/artists")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Artist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Artist> GetArtistAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<Artist>(
                new($"{SpotifyApiClient.BaseUrl}/artists/{id}"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<Album>> GetArtistAlbumsAsync(
            String id,
            AlbumGroups? includeGroups = null,
            CountryCode? market = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/artists/{id}/albums")
                .AppendJoinToQueryIfNotNull("include_groups", ',', includeGroups?.ToSpotifyStrings())
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString())
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<Album>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Track>> GetArtistTopTracksAsync(
            String id,
            CountryCode market,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/artists/{id}/top-tracks")
                .AppendToQuery("country", market);

            return base.SendAsync<IReadOnlyList<Track>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Artist>> GetArtistRelatedArtistsAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<IReadOnlyList<Artist>>(
                new($"{SpotifyApiClient.BaseUrl}/artists/{id}/related-artists"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyArtistsApi Implementation
        Task<IReadOnlyList<Artist>> ISpotifyArtistsApi.GetArtistsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.GetArtistsAsync(ids, null, cancellationToken);
        }

        Task<Artist> ISpotifyArtistsApi.GetArtistAsync(String id, CancellationToken cancellationToken)
        {
            return this.GetArtistAsync(id, null, cancellationToken);
        }

        Task<Paging<Album>> ISpotifyArtistsApi.GetArtistAlbumsAsync(
            String id,
            AlbumGroups? includeGroups,
            CountryCode? market,
            Int32? limit,
            Int32? offset,
            CancellationToken cancellationToken)
        {
            return this.GetArtistAlbumsAsync(id, includeGroups, market, limit, offset, null, cancellationToken);
        }

        Task<IReadOnlyList<Track>> ISpotifyArtistsApi.GetArtistTopTracksAsync(
            String id,
            CountryCode market,
            CancellationToken cancellationToken)
        {
            return this.GetArtistTopTracksAsync(id, market, null, cancellationToken);
        }

        Task<IReadOnlyList<Artist>> ISpotifyArtistsApi.GetArtistRelatedArtistsAsync(String id, CancellationToken cancellationToken)
        {
            return this.GetArtistRelatedArtistsAsync(id, null, cancellationToken);
        }
        #endregion
    }
}