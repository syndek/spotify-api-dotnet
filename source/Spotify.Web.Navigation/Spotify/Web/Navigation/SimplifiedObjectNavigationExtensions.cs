﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify.Web.Navigation
{
    public static class SimplifiedObjectNavigationExtensions
    {
        public static Task<Artist> GetFullDetailsAsync(
            this SimplifiedArtist artist,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            if (artist is Artist original)
            {
                return Task.FromResult(original);
            }
            
            return httpClient.GetAsync<Artist>(artist.Href, accessTokenProvider, cancellationToken);
        }

        public static Task<Album> GetFullDetailsAsync(
            this SimplifiedAlbum album,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            if (album is Album original)
            {
                return Task.FromResult(original);
            }
            
            return httpClient.GetAsync<Album>(album.Href, accessTokenProvider, cancellationToken);
        }

        public static Task<Track> GetFullDetailsAsync(
            this SimplifiedTrack track,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            if (track is Track original)
            {
                return Task.FromResult(original);
            }
            
            return httpClient.GetAsync<Track>(track.Href, accessTokenProvider, cancellationToken);
        }

        public static Task<Show> GetFullDetailsAsync(
            this SimplifiedShow show,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            if (show is Show original)
            {
                return Task.FromResult(original);
            }
            
            return httpClient.GetAsync<Show>(show.Href, accessTokenProvider, cancellationToken);
        }

        public static Task<Episode> GetFullDetailsAsync(
            this SimplifiedEpisode episode,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            if (episode is Episode original)
            {
                return Task.FromResult(original);
            }
            
            return httpClient.GetAsync<Episode>(episode.Href, accessTokenProvider, cancellationToken);
        }
    }
}
