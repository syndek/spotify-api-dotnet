using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class PlaylistConverterTests : JsonConverterTests<Playlist>
    {
        public override String TestJson => TestData.PlaylistJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new PlaylistConverter(),
                new FollowersConverter(),
                new ImageConverter(),
                new PagingConverterFactory(),
                new PlaylistTrackConverter(),
                new PlayableConverter(),
                new PublicUserConverter()
            }
        };
    }
}