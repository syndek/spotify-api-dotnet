using System;
using System.Collections.Generic;
using System.Text;

namespace Spotify.Web
{
    internal class SpotifyUriBuilder : object
    {
        private readonly string path;

        private StringBuilder? queryBuilder;

        public SpotifyUriBuilder(string path) : base()
        {
            this.path = path;
            this.queryBuilder = null;
        }

        private StringBuilder QueryBuilder => this.queryBuilder ??= new();

        public override string ToString()
        {
            if (this.queryBuilder is null)
            {
                return this.path;
            }

            return $"{this.path}?{this.queryBuilder}";
        }

        public SpotifyUriBuilder AppendToQuery<TElement>(string name, TElement element)
        {
            if (this.QueryBuilder.Length > 0)
            {
                this.QueryBuilder.Append('&');
            }

            this.QueryBuilder.Append(name).Append('=').Append(element);

            return this;
        }

        public SpotifyUriBuilder AppendToQueryIfNotNull<TElement>(string name, TElement? element) where TElement : class
        {
            if (element is not null)
            {
                this.AppendToQuery(name, element);
            }

            return this;
        }

        public SpotifyUriBuilder AppendToQueryIfNotNull<TElement>(string name, TElement? element) where TElement : struct
        {
            if (element is not null)
            {
                this.AppendToQuery(name, element.Value);
            }

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQuery<TElement>(string name, string separator, IEnumerable<TElement> elements)
        {
            if (this.QueryBuilder.Length > 0)
            {
                this.QueryBuilder.Append('&');
            }

            this.QueryBuilder.Append(name).Append('=').AppendJoin(separator, elements);

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQuery<TElement>(string name, char separator, IEnumerable<TElement> elements)
        {
            if (this.QueryBuilder.Length > 0)
            {
                this.QueryBuilder.Append('&');
            }

            this.QueryBuilder.Append(name).Append('=').AppendJoin(separator, elements);

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQueryIfNotNull<TElement>(string name, string separator, IEnumerable<TElement>? elements)
        {
            if (elements is not null)
            {
                this.AppendJoinToQuery(name, separator, elements);
            }

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQueryIfNotNull<TElement>(string name, char separator, IEnumerable<TElement>? elements)
        {
            if (elements is not null)
            {
                this.AppendJoinToQuery(name, separator, elements);
            }

            return this;
        }

        public Uri Build() => new(this.ToString());
    }
}