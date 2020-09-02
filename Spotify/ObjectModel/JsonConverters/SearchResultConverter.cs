using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class SearchResultConverter : JsonConverter<SearchResult>
    {
        internal static readonly SearchResultConverter Instance = new();

        private SearchResultConverter() : base() { }

        public override SearchResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            Paging<Artist>? artists = null;
            Paging<SimplifiedAlbum>? albums = null;
            Paging<Track>? tracks = null;
            Paging<SimplifiedShow>? shows = null;
            Paging<SimplifiedEpisode>? episodes = null;
            Paging<Playlist>? playlists = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                switch (reader.GetString())
                {
                    case "artists":
                        reader.Read(JsonTokenType.StartObject);
                        artists = PagingConverter<Artist>.Instance.Read(ref reader, typeof(Paging<Artist>), options);
                        break;
                    case "albums":
                        reader.Read(JsonTokenType.StartObject);
                        albums = PagingConverter<SimplifiedAlbum>.Instance.Read(ref reader, typeof(Paging<SimplifiedAlbum>), options);
                        break;
                    case "tracks":
                        reader.Read(JsonTokenType.StartObject);
                        tracks = PagingConverter<Track>.Instance.Read(ref reader, typeof(Paging<Track>), options);
                        break;
                    case "shows":
                        reader.Read(JsonTokenType.StartObject);
                        shows = PagingConverter<SimplifiedShow>.Instance.Read(ref reader, typeof(Paging<SimplifiedShow>), options);
                        break;
                    case "episodes":
                        reader.Read(JsonTokenType.StartObject);
                        episodes = PagingConverter<SimplifiedEpisode>.Instance.Read(ref reader, typeof(Paging<SimplifiedEpisode>), options);
                        break;
                    case "playlists":
                        reader.Read(JsonTokenType.StartObject);
                        playlists = PagingConverter<Playlist>.Instance.Read(ref reader, typeof(Paging<Playlist>), options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(artists, albums, tracks, shows, episodes, playlists);
        }

        public override void Write(Utf8JsonWriter writer, SearchResult value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}