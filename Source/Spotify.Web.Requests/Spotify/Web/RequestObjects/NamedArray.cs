using System.Collections;
using System.Collections.Generic;

namespace Spotify.Web.RequestObjects
{
    internal readonly struct NamedArray<TElement> : IReadOnlyList<TElement>
    {
        private readonly IReadOnlyList<TElement> elements;

        internal NamedArray(string name, IReadOnlyList<TElement> elements)
        {
            this.Name = name;
            this.elements = elements;
        }

        public TElement this[int index] => this.elements[index];

        public string Name { get; }
        public int Count => this.elements.Count;

        public IEnumerator<TElement> GetEnumerator() => this.elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.elements.GetEnumerator();
    }
}