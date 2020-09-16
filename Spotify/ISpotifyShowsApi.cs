using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about one or more shows from the Spotify catalog.
    /// </summary>
    public interface ISpotifyShowsApi
    {
        Task<IReadOnlyList<SimplifiedShow>> GetShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Show> GetShowAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedEpisode>> GetShowEpisodesAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}