using System.Collections;
using System.Collections.Generic;

namespace Spotify.Web.RequestObjects
{
    internal readonly struct NamedArray<TElement> : IReadOnlyList<TElement>
    {
        private readonly IReadOnlyList<TElement> elements;

        internal NamedArray(string name, IReadOnlyList<TElement> elements)
        {
            Name = name;
            this.elements = elements;
        }

        public TElement this[int index] => elements[index];

        public string Name { get; }
        public int Count => elements.Count;

        public IEnumerator<TElement> GetEnumerator() => elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => elements.GetEnumerator();
    }
}
