using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedShowConverterTests : JsonConverterTests<SimplifiedShow>
    {
        protected override string TestJson => TestData.SimplifiedShowJson;
        protected override JsonSerializerOptions SerializerOptions => new()
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
