using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about one or more albums from the Spotify catalog.
    /// </summary>
    public interface ISpotifyAlbumsApi
    {
        /// <summary>
        /// Asynchronously get multiple <see cref="Album"/> objects from the Spotify catalog.
        /// </summary>
        /// <param name="ids">
        /// An <see cref="IEnumerable{T}"/> of <see cref="String"/> objects
        /// representing the Spotify IDs of the <see cref="Album"/> objects to get.
        /// </param>
        /// <param name="market">
        /// An optional <see cref="CountryCode"/> to apply
        /// <see href="https://spotify.dev/documentation/general/guides/track-relinking-guide/">track relinking</see>.
        /// </param>
        /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> to use instead of the default, if set.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<IReadOnlyList<Album?>> GetAlbumsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get an <see cref="Album"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="String"/> representing the Spotify ID of the <see cref="Album"/> to get.</param>
        /// <param name="market">
        /// An optional <see cref="CountryCode"/> to apply
        /// <see href="https://spotify.dev/documentation/general/guides/track-relinking-guide/">track relinking</see>.
        /// </param>
        /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> to use instead of the default, if set.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<Album> GetAlbumAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get the tracks of an <see cref="Album"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="String"/> representing the Spotify ID of the <see cref="Album"/> to get the tracks of.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <param name="offset">The index of the first result to return.</param>
        /// <param name="market">
        /// An optional <see cref="CountryCode"/> to apply
        /// <see href="https://spotify.dev/documentation/general/guides/track-relinking-guide/">track relinking</see>.
        /// </param>
        /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> to use instead of the default, if set.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<Paging<SimplifiedTrack>> GetAlbumTracksAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}