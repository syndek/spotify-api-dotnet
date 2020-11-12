using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel.Serialization
{
    public sealed class SearchResultConverter : JsonConverter<SearchResult>
    {
        public override SearchResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var artistPagingConverter = options.GetConverter<Paging<Artist>>();
            var playlistPagingConverter = options.GetConverter<Paging<Playlist>>();
            var simplifiedAlbumPagingConverter = options.GetConverter<Paging<SimplifiedAlbum>>();
            var simplifiedEpisodePagingConverter = options.GetConverter<Paging<SimplifiedEpisode>>();
            var simplifiedShowPagingConverter = options.GetConverter<Paging<SimplifiedShow>>();
            var trackPagingConverter = options.GetConverter<Paging<Track>>();

            Paging<Artist>? artists = null;
            Paging<SimplifiedAlbum>? albums = null;
            Paging<Track>? tracks = null;
            Paging<SimplifiedShow>? shows = null;
            Paging<SimplifiedEpisode>? episodes = null;
            Paging<Playlist>? playlists = null;

            while (reader.Read())
            {
                if (reader.TokenType is JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType is not JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                var propertyName = reader.GetString();

                reader.Read(); // Read to next token.

                switch (propertyName)
                {
                    case "artists":
                        artists = artistPagingConverter.Read(ref reader, typeof(Paging<Artist>), options)!;
                        break;
                    case "albums":
                        albums = simplifiedAlbumPagingConverter.Read(ref reader, typeof(Paging<SimplifiedAlbum>), options)!;
                        break;
                    case "tracks":
                        tracks = trackPagingConverter.Read(ref reader, typeof(Paging<Track>), options)!;
                        break;
                    case "shows":
                        shows = simplifiedShowPagingConverter.Read(ref reader, typeof(Paging<SimplifiedShow>), options)!;
                        break;
                    case "episodes":
                        episodes = simplifiedEpisodePagingConverter.Read(ref reader, typeof(Paging<SimplifiedEpisode>), options)!;
                        break;
                    case "playlists":
                        playlists = playlistPagingConverter.Read(ref reader, typeof(Paging<Playlist>), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(artists, albums, tracks, shows, episodes, playlists);
        }

        public override void Write(Utf8JsonWriter writer, SearchResult value, JsonSerializerOptions options)
        {
            void WritePaging<TObject>(String propertyName, Paging<TObject>? paging, JsonConverter<Paging<TObject>> converter)
            {
                if (paging is not null)
                {
                    writer.WritePropertyName(propertyName);
                    converter.Write(writer, paging, options);
                }
                else
                {
                    writer.WriteNull(propertyName);
                }
            }

            var artistPagingConverter = options.GetConverter<Paging<Artist>>();
            var playlistPagingConverter = options.GetConverter<Paging<Playlist>>();
            var simplifiedAlbumPagingConverter = options.GetConverter<Paging<SimplifiedAlbum>>();
            var simplifiedEpisodePagingConverter = options.GetConverter<Paging<SimplifiedEpisode>>();
            var simplifiedShowPagingConverter = options.GetConverter<Paging<SimplifiedShow>>();
            var trackPagingConverter = options.GetConverter<Paging<Track>>();

            writer.WriteStartObject();
            WritePaging("artists", value.Artists, artistPagingConverter);
            WritePaging("albums", value.Albums, simplifiedAlbumPagingConverter);
            WritePaging("tracks", value.Tracks, trackPagingConverter);
            WritePaging("shows", value.Shows, simplifiedShowPagingConverter);
            WritePaging("episodes", value.Episodes, simplifiedEpisodePagingConverter);
            WritePaging("playlists", value.Playlists, playlistPagingConverter);
            writer.WriteEndObject();
        }
    }
}