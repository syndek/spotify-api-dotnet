using System;
using System.Collections;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record GenreSeedList : SpotifyObject, IReadOnlyList<String>
    {
        private readonly IReadOnlyList<String> genreSeeds;

        internal GenreSeedList(IReadOnlyList<String> genreSeeds)
        {
            this.genreSeeds = genreSeeds;
        }

        public String this[Int32 index] => this.genreSeeds[index];

        public Int32 Count => this.genreSeeds.Count;

        public IEnumerator<String> GetEnumerator() => this.genreSeeds.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}