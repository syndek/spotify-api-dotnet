using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents the results of a call to <see cref="ISpotifySearchApi.SearchAsync"/>.
    /// </summary>
    public record SearchResult : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResult"/> record with the specified values.
        /// </summary>
        /// <param name="artists">
        /// A <see cref="Paging{TItem}"/> of returned <see cref="Artist"/> objects, or <see langword="null"/> if not included.
        /// </param>
        /// <param name="albums">
        /// A <see cref="Paging{TItem}"/> of returned <see cref="SimplifiedAlbum"/> objects, or <see langword="null"/> if not included.
        /// </param>
        /// <param name="tracks">
        /// A <see cref="Paging{TItem}"/> of returned <see cref="Track"/> objects, or <see langword="null"/> if not included.
        /// </param>
        /// <param name="shows">
        /// A <see cref="Paging{TItem}"/> of returned <see cref="SimplifiedShow"/> objects, or <see langword="null"/> if not included.
        /// </param>
        /// <param name="episodes">
        /// A <see cref="Paging{TItem}"/> of returned <see cref="SimplifiedEpisode"/> objects, or <see langword="null"/> if not included.
        /// </param>
        /// <param name="playlists">
        /// A <see cref="Paging{TItem}"/> of returned <see cref="Playlist"/> objects, or <see langword="null"/> if not included.
        /// </param>
        public SearchResult(
            Paging<Artist>? artists,
            Paging<SimplifiedAlbum>? albums,
            Paging<Track>? tracks,
            Paging<SimplifiedShow>? shows,
            Paging<SimplifiedEpisode>? episodes,
            Paging<Playlist>? playlists)
        {
            Artists = artists;
            Albums = albums;
            Tracks = tracks;
            Shows = shows;
            Episodes = episodes;
            Playlists = playlists;
        }

        /// <summary>
        /// Gets or sets the returned artists of the <see cref="SearchResult"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of returned <see cref="Artist"/> objects, or <see langword="null"/> if not included.
        /// </returns>
        public Paging<Artist>? Artists { get; init; }
        /// <summary>
        /// Gets or sets the returned albums of the <see cref="SearchResult"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of returned <see cref="SimplifiedAlbum"/> objects, or <see langword="null"/> if not included.
        /// </returns>
        public Paging<SimplifiedAlbum>? Albums { get; init; }
        /// <summary>
        /// Gets or sets the returned tracks of the <see cref="SearchResult"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of returned <see cref="Track"/> objects, or <see langword="null"/> if not included.
        /// </returns>
        public Paging<Track>? Tracks { get; init; }
        /// <summary>
        /// Gets or sets the returned shows of the <see cref="SearchResult"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of returned <see cref="SimplifiedShow"/> objects, or <see langword="null"/> if not included.
        /// </returns>
        public Paging<SimplifiedShow>? Shows { get; init; }
        /// <summary>
        /// Gets or sets the returned episodes of the <see cref="SearchResult"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of returned <see cref="SimplifiedEpisode"/> objects, or <see langword="null"/> if not included.
        /// </returns>
        public Paging<SimplifiedEpisode>? Episodes { get; init; }
        /// <summary>
        /// Gets or sets the returned playlists of the <see cref="SearchResult"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of returned <see cref="Playlist"/> objects, or <see langword="null"/> if not included.
        /// </returns>
        public Paging<Playlist>? Playlists { get; init; }
    }
}