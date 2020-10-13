using System;
using System.Collections;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a container for a set of requested items that provides links to get further items.
    /// </summary>
    /// <typeparam name="TItem">The type of item that the <see cref="Paging{TItem}"/> contains.</typeparam>
    public record Paging<TItem> : SpotifyObject, IReadOnlyList<TItem>
    {
        /// <summary>
        /// Represents an empty <see cref="Paging{TItem}"/>. This field is read-only.
        /// </summary>
        public static readonly Paging<TItem> Empty = new(Array.Empty<TItem>(), 0, 0, 0, null!, null, null);

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
        public Paging(IReadOnlyList<TItem> items, Int32 total, Int32 limit, Int32 offset, Uri href, Uri? previous, Uri? next) : base()
        {
            this.items = new ImmutableValueArray<TItem>(items);
            this.Total = total;
            this.Limit = limit;
            this.Offset = offset;
            this.Href = href;
            this.Previous = previous;
            this.Next = next;
        }

        /// <summary>
        /// Gets the item at the specified <paramref name="index"/> in the <see cref="Paging{TItem}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get.</param>
        /// <returns>The item at the specified <paramref name="index"/> in the <see cref="Paging{TItem}"/>.</returns>
        public TItem this[Int32 index] => this.items[index];

        /// <summary>
        /// Gets or sets the maximum number of items available to return.
        /// </summary>
        /// <returns>The maximum number of items available to return.</returns>
        public Int32 Total { get; init; }
        /// <summary>
        /// Gets or sets the maximum number of items in the response (as set in the query or by default).
        /// </summary>
        /// <returns>The maximum number of items in the response.</returns>
        public Int32 Limit { get; init; }
        /// <summary>
        /// Gets or sets the offset of the items returned (as set in the query or by default).
        /// </summary>
        /// <returns>The offset of the items returned.</returns>
        public Int32 Offset { get; init; }
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
        public Int32 Count => this.items.Count;

        /// <summary>
        /// Gets an <see cref="IEnumerator{T}"/> that iterates through the items of the <see cref="Paging{TItem}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> that iterates through the items of the <see cref="Paging{TItem}"/>.</returns>
        public IEnumerator<TItem> GetEnumerator() => this.items.GetEnumerator();

        /// <summary>
        /// Gets an <see cref="IEnumerator"/> that iterates through the items of the <see cref="Paging{TItem}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> that iterates through the items of the <see cref="Paging{TItem}"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.items.GetEnumerator();
    }
}