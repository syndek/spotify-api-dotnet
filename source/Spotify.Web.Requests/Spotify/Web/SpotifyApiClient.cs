using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel.Serialization;
using Spotify.Web.Authorization;
using Spotify.Web.RequestObjects.Serialization;
using Spotify.Web.Serialization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a collection of endpoints defined by the Spotify Web API that share a common purpose.
    /// </summary>
    public abstract class SpotifyApiClient : IDisposable
    {
        internal const string BaseUrl = "https://api.spotify.com/v1";

        protected static readonly JsonSerializerOptions RequestObjectSerializerOptions = new()
        {
            Converters =
            {
                new PlaylistDetailsConverter(),
                new ReorderPlaylistItemsParametersConverter()
            }
        };

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
                new TrackConverter(),

                // Non-ObjectModel type converters.
                new NamedArrayConverterFactory()
            }
        };
        private static readonly JsonSerializerOptions ErrorSerializerOptions = new() { Converters = { new ErrorConverter() } };

        private readonly HttpClient httpClient;
        private AuthenticationHeaderValue? currentAuthenticationHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        protected SpotifyApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Gets or sets the default <see cref="IAccessTokenProvider"/> to use in API requests if one is not passed to a given request method.
        /// </summary>
        /// <returns>The default <see cref="IAccessTokenProvider"/> to use in API requests.</returns>
        public IAccessTokenProvider? DefaultAccessTokenProvider { get; set; }

        /// <summary>
        /// Disposes the <see cref="HttpClient"/> being used to make requests to the Spotify Web API.
        /// </summary>
        public void Dispose()
        {
            httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

        protected async Task<TObject> SendAsync<TObject>(
            Uri uri,
            HttpMethod method,
            HttpContent? content,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            using var message = await
                CreateAuthenticatedHttpRequestMessageAsync(uri, method, content, accessTokenProvider, cancellationToken)
                    .ConfigureAwait(false);

            var response = await httpClient
                .SendAsync(message, cancellationToken)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var returned = await response.Content
                    .ReadFromJsonAsync<TObject>(DefaultSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

                return returned!;
            }
            else
            {
                var error = await response.Content
                    .ReadFromJsonAsync<Error>(ErrorSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

                throw new HttpRequestException(error.Message, null, response.StatusCode);
            }
        }

        protected async Task SendAsync(
            Uri uri,
            HttpMethod method,
            HttpContent? content,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            using var message = await
                CreateAuthenticatedHttpRequestMessageAsync(uri, method, content, accessTokenProvider, cancellationToken)
                    .ConfigureAwait(false);

            var response = await httpClient
                .SendAsync(message, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content
                    .ReadFromJsonAsync<Error>(ErrorSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

                throw new HttpRequestException(error.Message, null, response.StatusCode);
            }
        }

        private async ValueTask<HttpRequestMessage> CreateAuthenticatedHttpRequestMessageAsync(
            Uri uri,
            HttpMethod method,
            HttpContent? content,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(method, uri) { Content = content };

            // Refresh current access token if necessary.
            var provider = accessTokenProvider ??
                DefaultAccessTokenProvider ??
                throw new InvalidOperationException($"No {nameof(IAccessTokenProvider)} provided to acquire access token from.");

            var accessToken = await provider.GetAccessTokenAsync(cancellationToken).ConfigureAwait(false);

            if (currentAuthenticationHeader?.Parameter != accessToken.Value)
            {
                // Cache an instance of AuthenticationHeaderValue so one doesn't need to be created every time a request is made.
                currentAuthenticationHeader = new("Bearer", accessToken.Value);
            }

            message.Headers.Authorization = currentAuthenticationHeader;
            return message;
        }
    }
}
