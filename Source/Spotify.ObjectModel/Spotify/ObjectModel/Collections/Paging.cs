using System;
using System.Collections;
using System.Collections.Generic;

namespace Spotify.ObjectModel.Collections
{
    /// <summary>
    /// Represents a container for a set of requested items that provides links to get further items.
    /// </summary>
    /// <typeparam name="TItem">The type of item that the <see cref="Paging{TItem}"/> contains.</typeparam>
    public record Paging<TItem> : SpotifyObject, IReadOnlyList<TItem>
    {
        private readonly IReadOnlyList<TItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging{TItem}"/> record with the specified values.
        /// </summary>
        /// <param name="items">The requested items.</param>
        /// <param name="total">The maximum number of items available to return.</param>
        /// <param name="limit">The maximum number of items in the response.</param>
        /// <param name="offset">The offset of the items returned.</param>
        /// <param name="href">A link to the Spotify Web API endpoint returning the full result of the request.</param>
        /// <param name="previous">The URL for the previous page of items, or <see langword="null"/> if none.</param>
        /// <param name="next">The URL for the next page of items, or <see langword="null"/> if none.</param>
        public Paging(IReadOnlyList<TItem> items, int total, int limit, int offset, Uri href, Uri? previous, Uri? next) : base()
        {
            this.items = new ImmutableValueArray<TItem>(items);
            Total = total;
            Limit = limit;
            Offset = offset;
            Href = href;
            Previous = previous;
            Next = next;
        }

        /// <summary>
        /// Gets the item at the specified <paramref name="index"/> in the <see cref="Paging{TItem}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get.</param>
        /// <returns>The item at the specified <paramref name="index"/> in the <see cref="Paging{TItem}"/>.</returns>
        public TItem this[int index] => items[index];

        /// <summary>
        /// Gets or sets the maximum number of items available to return.
        /// </summary>
        /// <returns>The maximum number of items available to return.</returns>
        public int Total { get; init; }
        /// <summary>
        /// Gets or sets the maximum number of items in the response (as set in the query or by default).
        /// </summary>
        /// <returns>The maximum number of items in the response.</returns>
        public int Limit { get; init; }
        /// <summary>
        /// Gets or sets the offset of the items returned (as set in the query or by default).
        /// </summary>
        /// <returns>The offset of the items returned.</returns>
        public int Offset { get; init; }
        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint returning the full result of the request.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint returning the full result of the request.</returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the URL for the previous page of items.
        /// </summary>
        /// <returns>The URL for the previous page of items, or <see langword="null"/> if none.</returns>
        public Uri? Previous { get; init; }
        /// <summary>
        /// Gets or sets the URL for the next page of items.
        /// </summary>
        /// <returns>The URL for the next page of items, or <see langword="null"/> if none.</returns>
        public Uri? Next { get; init; }
        /// <summary>
        /// Gets or sets the number of items contained in the <see cref="Paging{TItem}"/>.
        /// </summary>
        /// <returns>The number of items contained in the <see cref="Paging{TItem}"/>.</returns>
        public int Count => items.Count;

        /// <summary>
        /// Gets an <see cref="IEnumerator{T}"/> that iterates through the items of the <see cref="Paging{TItem}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> that iterates through the items of the <see cref="Paging{TItem}"/>.</returns>
        public IEnumerator<TItem> GetEnumerator() => items.GetEnumerator();

        /// <summary>
        /// Gets an <see cref="IEnumerator"/> that iterates through the items of the <see cref="Paging{TItem}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> that iterates through the items of the <see cref="Paging{TItem}"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
    }
}