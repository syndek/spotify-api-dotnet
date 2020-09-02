using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web;
using Spotify.Web.Authorization;

namespace Spotify.Navigation
{
    public static class PagingNavigationExtensions
    {
        public static async Task<Paging<TObject>> GetPreviousAsync<TObject>(
            this Paging<TObject> paging,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            if (paging.Previous is null)
            {
                throw new InvalidOperationException("Paging object contains no 'previous' URI.");
            }

            return await httpClient
                .GetAsync<Paging<TObject>>(paging.Previous, accessTokenProvider, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Paging<TObject>> GetNextAsync<TObject>(
            this Paging<TObject> paging,
            HttpClient httpClient,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            if (paging.Next is null)
            {
                throw new InvalidOperationException("Paging object contains no 'next' URI.");
            }

            return await httpClient
                .GetAsync<Paging<TObject>>(paging.Next, accessTokenProvider, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}