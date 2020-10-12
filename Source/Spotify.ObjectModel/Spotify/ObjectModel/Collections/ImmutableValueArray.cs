using System;
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

        public TElement this[Int32 index] => this.elements[index];

        public Int32 Count => this.elements.Count;

        public override Boolean Equals(Object? obj) => (obj is ImmutableValueArray<TElement> array) && this.Equals(array);

        public override Int32 GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var element in this)
            {
                hashCode.Add(element);
            }

            return hashCode.ToHashCode();
        }

        public Boolean Equals(ImmutableValueArray<TElement> other)
        {
            if (this.Count != other.Count)
            {
                return false;
            }

            for (var i = 0; i < this.Count; i++)
            {
                if (this[i]?.Equals(other[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<TElement> GetEnumerator() => this.elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.elements.GetEnumerator();

        public static Boolean operator ==(ImmutableValueArray<TElement>? left, ImmutableValueArray<TElement>? right) => (left, right) switch
        {
            (null, null) => true,
            (not null, not null) => left.Equals(right),
            _ => false
        };

        public static Boolean operator !=(ImmutableValueArray<TElement>? left, ImmutableValueArray<TElement>? right) => !(left == right);
    }
}