using System;
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
    }
}