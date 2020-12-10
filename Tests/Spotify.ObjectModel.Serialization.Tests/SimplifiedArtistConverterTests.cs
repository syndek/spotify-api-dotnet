using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedArtistConverterTests : JsonConverterTests<SimplifiedArtist>
    {
        public override string TestJson => TestData.SimplifiedArtistJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters = { new SimplifiedArtistConverter() }
        };
    }
}