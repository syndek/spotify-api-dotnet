using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about one or more episodes from the Spotify catalog.
    /// </summary>
    public interface ISpotifyEpisodesApi
    {
        /// <summary>
        /// Asynchronously get multiple <see cref="Episode"/> objects from the Spotify catalog.
        /// </summary>
        /// <param name="ids">
        /// An <see cref="IEnumerable{T}"/> of <see cref="String"/> objects
        /// representing the Spotify IDs of the <see cref="Episode"/> objects to get.
        /// </param>
        /// <param name="market">An optional <see cref="CountryCode"/> to limit results to a certain market only.</param>
        /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> to use instead of the default, if set.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<IReadOnlyList<Episode>> GetEpisodesAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get an <see cref="Episode"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="String"/> representing the Spotify ID of the <see cref="Episode"/> to get.</param>
        /// <param name="market">An optional <see cref="CountryCode"/> to limit results to a certain market only.</param>
        /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> to use instead of the default, if set.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<Episode> GetEpisodeAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}