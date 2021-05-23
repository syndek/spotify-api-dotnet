using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;

namespace Spotify
{
    /// <summary>
    /// Defines methods for getting recommendations, and playlists and new album releases featured on Spotify’s Browse tab.
    /// </summary>
    public interface ISpotifyBrowseApi
    {
        Task<Paging<Category>> GetCategoriesAsync(
            CountryCode? country = null,
            string? locale = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);

        Task<Category> GetCategoryAsync(
            string id,
            CountryCode? country = null,
            string? locale = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetCategoryPlaylistsAsync(
            string id,
            CountryCode? country = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);

        Task<Recommendations> GetRecommendationsAsync(
            IEnumerable<string> seedArtists,
            IEnumerable<string> seedTracks,
            IEnumerable<string> seedGenres,
            int? limit = null,
            CountryCode? market = null,
            TuneableTrackAttributes? minValues = null,
            TuneableTrackAttributes? maxValues = null,
            TuneableTrackAttributes? targetValues = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<string>> GetRecommendationGenresAsync(
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedAlbum>> GetNewReleasesAsync(
            CountryCode? country = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetFeaturedPlaylistsAsync(
            string? locale = null,
            CountryCode? country = null,
            DateTime? timestamp = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);
    }
}
