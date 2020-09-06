namespace Spotify.ObjectModel
{
    public record SearchResult : SpotifyObject
    {
        internal SearchResult(
            Paging<Artist>? artists,
            Paging<SimplifiedAlbum>? albums,
            Paging<Track>? tracks,
            Paging<SimplifiedShow>? shows,
            Paging<SimplifiedEpisode>? episodes,
            Paging<Playlist>? playlists) : base()
        {
            this.Artists = artists;
            this.Albums = albums;
            this.Tracks = tracks;
            this.Shows = shows;
            this.Episodes = episodes;
            this.Playlists = playlists;
        }

        public Paging<Artist>? Artists { get; init; }
        public Paging<SimplifiedAlbum>? Albums { get; init; }
        public Paging<Track>? Tracks { get; init; }
        public Paging<SimplifiedShow>? Shows { get; init; }
        public Paging<SimplifiedEpisode>? Episodes { get; init; }
        public Paging<Playlist>? Playlists { get; init; }
    }
}