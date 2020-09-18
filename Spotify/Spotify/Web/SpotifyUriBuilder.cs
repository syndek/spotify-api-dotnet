using System;
using System.Collections.Generic;
using System.Text;

namespace Spotify.Web
{
    internal class SpotifyUriBuilder : Object
    {
        private readonly String path;

        private StringBuilder? queryBuilder;

        internal SpotifyUriBuilder(String path) : base()
        {
            this.path = path;
            this.queryBuilder = null;
        }

        private StringBuilder QueryBuilder => this.queryBuilder ??= new();

        public override String ToString()
        {
            if (this.queryBuilder is null)
            {
                return this.path;
            }

            return $"{this.path}?{this.queryBuilder}";
        }

        public SpotifyUriBuilder AppendToQuery<TElement>(String name, TElement element)
        {
            if (this.QueryBuilder.Length > 0)
            {
                this.QueryBuilder.Append('&');
            }

            this.QueryBuilder.Append(name).Append('=').Append(element);

            return this;
        }

        public SpotifyUriBuilder AppendToQueryIfNotNull<TElement>(String name, TElement? element) where TElement : class
        {
            if (element is not null)
            {
                this.AppendToQuery(name, element);
            }

            return this;
        }

        public SpotifyUriBuilder AppendToQueryIfNotNull<TElement>(String name, TElement? element) where TElement : struct
        {
            if (element is not null)
            {
                this.AppendToQuery(name, element.Value);
            }

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQuery<TElement>(String name, String separator, IEnumerable<TElement> elements)
        {
            if (this.QueryBuilder.Length > 0)
            {
                this.QueryBuilder.Append('&');
            }

            this.QueryBuilder.Append(name).Append('=').AppendJoin(separator, elements);

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQuery<TElement>(String name, Char separator, IEnumerable<TElement> elements)
        {
            if (this.QueryBuilder.Length > 0)
            {
                this.QueryBuilder.Append('&');
            }

            this.QueryBuilder.Append(name).Append('=').AppendJoin(separator, elements);

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQueryIfNotNull<TElement>(String name, String separator, IEnumerable<TElement>? elements)
        {
            if (elements is not null)
            {
                this.AppendJoinToQuery(name, separator, elements);
            }

            return this;
        }

        public SpotifyUriBuilder AppendJoinToQueryIfNotNull<TElement>(String name, Char separator, IEnumerable<TElement>? elements)
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