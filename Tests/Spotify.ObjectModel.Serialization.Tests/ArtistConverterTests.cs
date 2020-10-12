
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ArtistConverterTests
    {
        // Artist representation of the JSON in Resources/TestData.resx.
        private static readonly Artist Artist = new(
            "4pb4rqWSoGUgxm63xmJ8xc",
            new("spotify:artist:4pb4rqWSoGUgxm63xmJ8xc"),
            new("https://api.spotify.com/v1/artists/4pb4rqWSoGUgxm63xmJ8xc"),
            "Madeon",
            new List<Image>
            {
                new(new("https://i.scdn.co/image/426e0d784cfc0dfe8f3275e32850223232835c14"), 640, 640),
                new(new("https://i.scdn.co/image/d12113789d3953523b7b2c2f0ef53871605fed1c"), 320, 320),
                new(new("https://i.scdn.co/image/3f4c99a2932c2e21fc966123050cd92fe4ff0c15"), 160, 160)
            },
            new(null, 542633),
            new List<String>
            {
                "big room",
                "complextro",
                "edm",
                "electro house",
                "electropop",
                "filter house",
                "future bass",
                "nantes indie"
            },
            65,
            new Dictionary<String, Uri>
            {
                { "spotify", new("https://open.spotify.com/artist/4pb4rqWSoGUgxm63xmJ8xc") }
            });


        [TestMethod]
        public void DeserializeArtist()
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new ArtistConverter(),
                    new FollowersConverter(),
                    new ImageConverter()
                }
            };

            CompareArtists(
                expected: Artist,
                actual: JsonSerializer.Deserialize<Artist>(TestData.ArtistJson, options));
        }

        [TestMethod]
        public void SerializeAndDeserializeArtist()
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new ArtistConverter(),
                    new FollowersConverter(),
                    new ImageConverter()
                }
            };

            var serialized = JsonSerializer.Serialize(Artist, options);

            CompareArtists(
                expected: Artist,
                actual: JsonSerializer.Deserialize<Artist>(serialized, options));
        }

        private static void CompareArtists(Artist expected, Artist actual)
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