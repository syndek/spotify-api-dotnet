using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more shows from the Spotify catalog.
    /// </summary>
    public class SpotifyShowsApiClient : SpotifyApiClient, ISpotifyShowsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyShowsApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyShowsApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc/>
        public Task<IReadOnlyList<SimplifiedShow>> GetShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task GetShowAsync(
            String id,
            CountryCode? market = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Paging<SimplifiedEpisode>> GetShowEpisodesAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}