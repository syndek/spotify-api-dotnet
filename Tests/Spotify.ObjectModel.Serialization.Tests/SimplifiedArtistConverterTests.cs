using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedArtistConverterTests : JsonConverterTests<SimplifiedArtist>
    {
        protected override string TestJson => TestData.SimplifiedArtistJson;
        protected override JsonSerializerOptions SerializerOptions => new() { Converters = { new SimplifiedArtistConverter() } };
    }
}
