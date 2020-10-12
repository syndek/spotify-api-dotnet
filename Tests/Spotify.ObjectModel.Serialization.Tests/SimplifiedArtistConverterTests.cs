using System;
using System.Linq;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedArtistConverterTests : JsonConverterTests<SimplifiedArtist>
    {
        public override String TestJson => TestData.SimplifiedArtistJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters = { new SimplifiedArtistConverter() }
        };

        protected override void CompareObjects(SimplifiedArtist expected, SimplifiedArtist actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Uri, actual.Uri);
            Assert.AreEqual(expected.Href, actual.Href);
            Assert.AreEqual(expected.Name, actual.Name);
            CollectionAssert.AreEquivalent(expected.ExternalUrls.ToArray(), actual.ExternalUrls.ToArray());
        }
    }
}