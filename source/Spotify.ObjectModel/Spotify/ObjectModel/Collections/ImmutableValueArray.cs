﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.ObjectModel.Collections
{
    public readonly struct ImmutableValueArray<TElement> : IEquatable<ImmutableValueArray<TElement>>, IReadOnlyList<TElement>
    {
        private readonly IReadOnlyList<TElement> elements;

        public ImmutableValueArray(IEnumerable<TElement> elements)
        {
            this.elements = elements.ToArray();
        }

        public TElement this[int index] => elements[index];

        public int Count => elements.Count;

        public override bool Equals(object? obj) => obj is ImmutableValueArray<TElement> array && Equals(array);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var element in this)
            {
                hashCode.Add(element);
            }

            return hashCode.ToHashCode();
        }

        public bool Equals(ImmutableValueArray<TElement> other)
        {
            if (Count != other.Count)
            {
                return false;
            }

            for (var i = 0; i < Count; i++)
            {
                if (this[i]?.Equals(other[i]) is false)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<TElement> GetEnumerator() => elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => elements.GetEnumerator();

        public static bool operator ==(ImmutableValueArray<TElement>? left, ImmutableValueArray<TElement>? right) => (left, right) switch
        {
            (null, null) => true,
            (not null, not null) => left.Equals(right),
            _ => false
        };

        public static bool operator !=(ImmutableValueArray<TElement>? left, ImmutableValueArray<TElement>? right) => !(left == right);
    }
}
