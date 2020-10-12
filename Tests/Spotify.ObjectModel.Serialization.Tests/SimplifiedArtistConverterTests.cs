using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedArtistConverterTests
    {
        // SimplifiedArtist representation of the JSON in Resources/TestData.resx.
        private static readonly SimplifiedArtist SimplifiedArtist = new(
            "4pb4rqWSoGUgxm63xmJ8xc",
            new("spotify:artist:4pb4rqWSoGUgxm63xmJ8xc"),
            new("https://api.spotify.com/v1/artists/4pb4rqWSoGUgxm63xmJ8xc"),
            "Madeon",
            new Dictionary<String, Uri>
            {
                { "spotify", new("https://open.spotify.com/artist/4pb4rqWSoGUgxm63xmJ8xc") }
            });


        [TestMethod]
        public void DeserializeSimplifiedArtist()
        {
            var options = new JsonSerializerOptions { Converters = { new SimplifiedArtistConverter() } };

            CompareSimplifiedArtists(
                expected: SimplifiedArtist,
                actual: JsonSerializer.Deserialize<SimplifiedArtist>(TestData.SimplifiedArtistJson, options));
        }

        [TestMethod]
        public void SerializeAndDeserializeSimplifiedArtist()
        {
            var options = new JsonSerializerOptions { Converters = { new SimplifiedArtistConverter() } };

            var serialized = JsonSerializer.Serialize(SimplifiedArtist, options);

            CompareSimplifiedArtists(
                expected: SimplifiedArtist,
                actual: JsonSerializer.Deserialize<SimplifiedArtist>(serialized, options));
        }

        private static void CompareSimplifiedArtists(SimplifiedArtist expected, SimplifiedArtist actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Uri, actual.Uri);
            Assert.AreEqual(expected.Href, actual.Href);
            Assert.AreEqual(expected.Name, actual.Name);
            CollectionAssert.AreEquivalent(expected.ExternalUrls.ToArray(), actual.ExternalUrls.ToArray());
        }
    }
}