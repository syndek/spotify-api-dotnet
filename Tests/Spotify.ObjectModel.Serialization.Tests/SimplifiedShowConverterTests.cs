using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedShowConverterTests : JsonConverterTests<SimplifiedShow>
    {
        public override String TestJson => TestData.SimplifiedShowJson;
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