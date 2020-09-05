using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for getting recommendations, and playlists and new album releases featured on Spotify’s Browse tab.
    /// </summary>
    public interface ISpotifyBrowseApi
    {
        Task<Paging<Category>> GetCategoriesAsync(
            CountryCode? country = null,
            String? locale = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Category> GetCategoryAsync(
            String id,
            CountryCode? country = null,
            String? locale = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetCategoryPlaylistsAsync(
            String id,
            CountryCode? country = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Recommendations> GetRecommendationsAsync(
            IReadOnlyList<String> seedArtists,
            IReadOnlyList<String> seedTracks,
            IReadOnlyList<String> seedGenres,
            Int32? limit = null,
            CountryCode? market = null,
            TuneableTrackAttributes? minValues = null,
            TuneableTrackAttributes? maxValues = null,
            TuneableTrackAttributes? targetValues = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<GenreSeedList> GetRecommendationGenresAsync(
            IAccessTokenProvider? accessTokenProvider = null, 
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedAlbum>> GetNewReleasesAsync(
            CountryCode? country = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetFeaturedPlaylistsAsync(
            String? locale = null,
            CountryCode? country = null,
            DateTime? timestamp = null,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}