using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedShowConverterTests : JsonConverterTests<SimplifiedShow>
    {
        public override string TestJson => TestData.SimplifiedShowJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new SimplifiedShowConverter(),
                new CopyrightConverter(),
                new CountryCodeConverter(),
                new ImageConverter()
            }
        };
    }
}