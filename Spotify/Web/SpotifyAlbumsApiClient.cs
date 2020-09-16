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
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more albums from the Spotify catalog.
    /// </summary>
    public class SpotifyAlbumsApiClient : SpotifyApiClient, ISpotifyAlbumsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAlbumsApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyAlbumsApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Album?>> GetAlbumsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/albums")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<IReadOnlyList<Album?>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Album> GetAlbumAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/albums/{id}")
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Album>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<SimplifiedTrack>> GetAlbumTracksAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/albums/{id}/tracks")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Paging<SimplifiedTrack>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }
    }
}