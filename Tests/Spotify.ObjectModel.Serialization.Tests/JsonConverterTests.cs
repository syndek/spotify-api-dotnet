using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spotify.ObjectModel.Serialization.Tests
{
    public abstract class JsonConverterTests<TObject> : Object
    {
        public abstract String TestJson { get; }
        public abstract JsonSerializerOptions SerializerOptions { get; }

        [TestMethod]
        public void RoundTripSerialization()
        {
            var deserialized = JsonSerializer.Deserialize<TObject>(this.TestJson, this.SerializerOptions);
            var reserialized = JsonSerializer.Serialize(deserialized, this.SerializerOptions);

            Assert.AreEqual(
                expected: deserialized,
                actual: JsonSerializer.Deserialize<TObject>(reserialized, this.SerializerOptions));
        }
    }
}