using System;
using System.Collections.Generic;
using System.Text;

namespace Spotify.Web
{
    internal class SpotifyUriBuilder
    {
        private readonly string path;

        private StringBuilder? queryBuilder;

        public SpotifyUriBuilder(string path)
        {
            this.path = path;
            queryBuilder = null;
        }

        private StringBuilder QueryBuilder => queryBuilder ??= new();

        public override string ToString()
        {
            if (queryBuilder is null)
            {
                return path;
            }

            return $"{path}?{queryBuilder}";
        }

        public SpotifyUriBuilder AppendToQuery<TElement>(string name, TElement element)
        {
            if (QueryBuilder.Length > 0)
            {
                QueryBuilder.Append('&');
            }

            QueryBuilder.Append(name).Append('=').Append(element);

            return this;
        }

        public SpotifyUriBuilder AppendToQueryIfNotNull<TElement>(string name, TElement? element) where TElement : class
        {
            if (element is not null)
            {
                AppendToQuery(name, element);
            }

            return this;
        }

        public SpotifyUriBuilder AppendToQueryIfNotNull<TElement>(string name, TElement? element) where TElement : struct
        {
            if (element is not null)
            {
                AppendToQuery(name, element.Value);
            }

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQuery<TElement>(string name, string separator, IEnumerable<TElement> elements)
        {
            if (QueryBuilder.Length > 0)
            {
                QueryBuilder.Append('&');
            }

            QueryBuilder.Append(name).Append('=').AppendJoin(separator, elements);

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQuery<TElement>(string name, char separator, IEnumerable<TElement> elements)
        {
            if (QueryBuilder.Length > 0)
            {
                QueryBuilder.Append('&');
            }

            QueryBuilder.Append(name).Append('=').AppendJoin(separator, elements);

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQueryIfNotNull<TElement>(string name, string separator, IEnumerable<TElement>? elements)
        {
            if (elements is not null)
            {
                AppendJoinToQuery(name, separator, elements);
            }

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQueryIfNotNull<TElement>(string name, char separator, IEnumerable<TElement>? elements)
        {
            if (elements is not null)
            {
                AppendJoinToQuery(name, separator, elements);
            }

            return this;
        }

        public Uri Build() => new(ToString());
    }
}