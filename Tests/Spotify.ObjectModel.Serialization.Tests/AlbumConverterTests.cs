using System;
using System.Linq;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class AlbumConverterTests : JsonConverterTests<Album>
    {
        public override String TestJson => TestData.AlbumJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new AlbumConverter(),
                new ImageConverter(),
                new SimplifiedArtistConverter(),
                new PagingConverterFactory(),
                new SimplifiedTrackConverter(),
                new CountryCodeConverter(),
                new CopyrightConverter()
            }
        };

        protected override void CompareObjects(Album expected, Album actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Uri, actual.Uri);
            Assert.AreEqual(expected.Href, actual.Href);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Type, actual.Type);
            CollectionAssert.AreEqual(expected.Images.ToArray(), actual.Images.ToArray());
            CollectionAssert.AreEqual(expected.Artists.ToArray(), actual.Artists.ToArray());
            Assert.AreEqual(expected.ReleaseDate, actual.ReleaseDate);
            Assert.AreEqual(expected.ReleaseDatePrecision, actual.ReleaseDatePrecision);
            CollectionAssert.That.PagingsAreEqual(expected.Tracks, actual.Tracks);
            CollectionAssert.AreEqual(expected.Genres.ToArray(), actual.Genres.ToArray());
            Assert.AreEqual(expected.Popularity, actual.Popularity);
            CollectionAssert.AreEqual(expected.AvailableMarkets.ToArray(), actual.AvailableMarkets.ToArray());
            Assert.AreEqual(expected.Label, actual.Label);
            CollectionAssert.AreEqual(expected.Copyrights.ToArray(), actual.Copyrights.ToArray());
            CollectionAssert.AreEquivalent(expected.ExternalIds.ToArray(), actual.ExternalIds.ToArray());
            CollectionAssert.AreEquivalent(expected.ExternalUrls.ToArray(), actual.ExternalUrls.ToArray());
        }
    }
}