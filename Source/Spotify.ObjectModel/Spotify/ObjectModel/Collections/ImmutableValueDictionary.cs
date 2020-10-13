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
            this.dictionary = new Dictionary<TKey, TValue>(elements);
        }

        public TValue this[TKey key] => this.dictionary[key];

        public IEnumerable<TKey> Keys => this.dictionary.Keys;
        public IEnumerable<TValue> Values => this.dictionary.Values;
        public Int32 Count => this.dictionary.Count;

        public override Boolean Equals(Object? obj) => (obj is ImmutableValueDictionary<TKey, TValue> dictionary) && this.Equals(dictionary);

        // This probably needs to be done better, but will work 'good enough' for now.
        public override Int32 GetHashCode() => this.dictionary.GetHashCode();

        public Boolean ContainsKey(TKey key) => this.dictionary.ContainsKey(key);

        public Boolean Equals(ImmutableValueDictionary<TKey, TValue> other)
        {
            if (this.Count != other.Count)
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

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.dictionary.GetEnumerator();

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => this.dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => this.dictionary.GetEnumerator();

        public static Boolean operator ==(ImmutableValueDictionary<TKey, TValue>? left, ImmutableValueDictionary<TKey, TValue>? right) => (left, right) switch
        {
            (null, null) => true,
            (not null, not null) => left.Equals(right),
            _ => false
        };

        public static Boolean operator !=(ImmutableValueDictionary<TKey, TValue>? left, ImmutableValueDictionary<TKey, TValue>? right) => !(left == right);
    }
}