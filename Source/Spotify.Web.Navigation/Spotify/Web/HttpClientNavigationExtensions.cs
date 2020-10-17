using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel.Serialization;
using Spotify.Web.Authorization;
using Spotify.Web.Serialization;

namespace Spotify.Web
{
    internal static class HttpClientNavigationExtensions : Object
    {
        private static readonly JsonSerializerOptions DefaultSerializerOptions = new()
        {
            Converters =
            {
                new AlbumConverter(),
                new ArtistConverter(),
                new AudioAnalysisConverter(),
                new AudioFeaturesConverter(),
                new CategoryConverter(),
                new ContextConverter(),
                new CopyrightConverter(),
                new CountryCodeConverter(),
                new EpisodeConverter(),
                new FollowersConverter(),
                new ImageConverter(),
                new PagingConverterFactory(),
                new PlayableConverter(),
                new PlaylistConverter(),
                new PlaylistTrackConverter(),
                new PrivateUserConverter(),
                new PublicUserConverter(),
                new RecommendationsConverter(),
                new RecommendationSeedConverter(),
                new ResumePointConverter(),
                new SavedConverterFactory(),
                new SearchResultConverter(),
                new SectionConverter(),
                new SegmentConverter(),
                new ShowConverter(),
                new SimplifiedAlbumConverter(),
                new SimplifiedArtistConverter(),
                new SimplifiedEpisodeConverter(),
                new SimplifiedPlaylistConverter(),
                new SimplifiedShowConverter(),
                new SimplifiedTrackConverter(),
                new TimeIntervalConverter(),
                new TrackConverter()
            }
        };
        private static readonly JsonSerializerOptions ErrorSerializerOptions = new()
        {
            Converters = { new ErrorConverter() }
        };

        internal static async Task<TObject> GetAsync<TObject>(
            this HttpClient httpClient,
            Uri uri,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var accessToken = await accessTokenProvider
                .GetAccessTokenAsync(cancellationToken)
                .ConfigureAwait(false);

            using var message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Authorization = new("Bearer", accessToken.Value);

            var response = await httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var returned = await response.Content
                    .ReadFromJsonAsync<TObject>(HttpClientNavigationExtensions.DefaultSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

                return returned!;
            }
            else
            {
                var error = await response.Content
                    .ReadFromJsonAsync<Error>(HttpClientNavigationExtensions.ErrorSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

                throw new HttpRequestException(error.Message, null, response.StatusCode);
            }
        }
    }
}