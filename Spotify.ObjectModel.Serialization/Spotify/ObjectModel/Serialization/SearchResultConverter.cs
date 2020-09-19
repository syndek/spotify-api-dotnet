using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class SearchResultConverter : JsonConverter<SearchResult>
    {
        public static readonly SearchResultConverter Instance = new();

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
                        artists = reader.ReadPaging<Artist>();
                        break;
                    case "albums":
                        reader.Read(JsonTokenType.StartObject);
                        albums = reader.ReadPaging<SimplifiedAlbum>();
                        break;
                    case "tracks":
                        reader.Read(JsonTokenType.StartObject);
                        tracks = reader.ReadPaging<Track>();
                        break;
                    case "shows":
                        reader.Read(JsonTokenType.StartObject);
                        shows = reader.ReadPaging<SimplifiedShow>();
                        break;
                    case "episodes":
                        reader.Read(JsonTokenType.StartObject);
                        episodes = reader.ReadPaging<SimplifiedEpisode>();
                        break;
                    case "playlists":
                        reader.Read(JsonTokenType.StartObject);
                        playlists = reader.ReadPaging<Playlist>();
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