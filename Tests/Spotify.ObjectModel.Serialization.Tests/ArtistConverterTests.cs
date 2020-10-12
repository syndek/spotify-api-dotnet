using System;
using System.Linq;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ArtistConverterTests : JsonConverterTests<Artist>
    {
        public override String TestJson => TestData.ArtistJson;
        public override JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
        {
            Converters =
            {
                new ArtistConverter(),
                new FollowersConverter(),
                new ImageConverter()
            }
        };

        protected override void CompareObjects(Artist expected, Artist actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Uri, actual.Uri);
            Assert.AreEqual(expected.Href, actual.Href);
            Assert.AreEqual(expected.Name, actual.Name);
            CollectionAssert.AreEquivalent(expected.Images.ToArray(), actual.Images.ToArray());
            Assert.AreEqual(expected.Followers, actual.Followers);
            CollectionAssert.AreEquivalent(expected.Genres.ToArray(), actual.Genres.ToArray());
            Assert.AreEqual(expected.Popularity, actual.Popularity);
            CollectionAssert.AreEquivalent(expected.ExternalUrls.ToArray(), actual.ExternalUrls.ToArray());
        }
    }
}