using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Spotify.ObjectModel.Collections
{
    public readonly struct ImmutableValueDictionary<TKey, TValue> : IEquatable<ImmutableValueDictionary<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue> where TKey : notnull
    {
        private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

        public ImmutableValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> elements)
        {
            dictionary = new Dictionary<TKey, TValue>(elements);
        }

        public TValue this[TKey key] => dictionary[key];

        public IEnumerable<TKey> Keys => dictionary.Keys;
        public IEnumerable<TValue> Values => dictionary.Values;
        public int Count => dictionary.Count;

        public override bool Equals(object? obj) => obj is ImmutableValueDictionary<TKey, TValue> dictionary && Equals(dictionary);

        // This probably needs to be done better, but will work 'good enough' for now.
        public override int GetHashCode() => dictionary.GetHashCode();

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public bool Equals(ImmutableValueDictionary<TKey, TValue> other)
        {
            if (Count != other.Count)
            {
                return false;
            }

            var valueComparer = EqualityComparer<TValue>.Default;

            foreach (var element in this)
            {
                if (!other.TryGetValue(element.Key, out var value))
                {
                    return false;
                }

                if (!valueComparer.Equals(element.Value, value))
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();

        public static bool operator ==(ImmutableValueDictionary<TKey, TValue>? left, ImmutableValueDictionary<TKey, TValue>? right) => (left, right) switch
        {
            (null, null) => true,
            (not null, not null) => left.Equals(right),
            _ => false
        };

        public static bool operator !=(ImmutableValueDictionary<TKey, TValue>? left, ImmutableValueDictionary<TKey, TValue>? right) => !(left == right);
    }
}
