using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about one or more artists from the Spotify catalog.
    /// </summary>
    public interface ISpotifyArtistsApi
    {
        /// <summary>
        /// Asynchronously get multiple <see cref="Artist"/> objects from the Spotify catalog.
        /// </summary>
        /// <param name="ids">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> objects
        /// representing the Spotify IDs of the <see cref="Artist"/> objects to get.
        /// </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<IReadOnlyList<Artist>> GetArtistsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<Artist> GetArtistAsync(
            string id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get the albums of an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get the albums of.</param>
        /// <param name="includeGroups">The <see cref="AlbumGroups"/> that the result should be filtered to include.</param>
        /// <param name="market">An optional <see cref="CountryCode"/> to limit the result to one particular geographical market.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <param name="offset">The index of the first result to return.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<Paging<SimplifiedAlbum>> GetArtistAlbumsAsync(
            string id,
            AlbumGroups? includeGroups = null,
            CountryCode? market = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get the top tracks of an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">
        /// A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get the top tracks of.
        /// </param>
        /// <param name="market">A <see cref="CountryCode"/> representing the market to get the top tracks for.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<IReadOnlyList<Track>> GetArtistTopTracksAsync(
            string id,
            CountryCode market,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get the related artists of an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">
        /// A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get the related artists of.
        /// </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<IReadOnlyList<Artist>> GetArtistRelatedArtistsAsync(
            string id,
            CancellationToken cancellationToken = default);
    }
}
