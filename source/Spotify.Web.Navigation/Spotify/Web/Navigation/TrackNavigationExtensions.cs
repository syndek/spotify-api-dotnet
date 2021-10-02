using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify.Web.Navigation
{
    public static class TrackNavigationExtensions
    {
        public static Task<Track> GetTrackAsync(
            this AudioFeatures audioFeatures,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            return httpClient.GetAsync<Track>(audioFeatures.TrackHref, accessTokenProvider, cancellationToken);
        }

        public static Task<Track> GetTrackAsync(
            this Context context,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            return httpClient.GetAsync<Track>(context.Href, accessTokenProvider, cancellationToken);
        }
    }
}
