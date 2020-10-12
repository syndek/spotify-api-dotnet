using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spotify.ObjectModel.Serialization.Tests
{
    internal static class AssertionExtensions : Object
    {
        internal static void PagingsAreEqual<TObject>(this CollectionAssert assert, Paging<TObject> expected, Paging<TObject> actual)
        {
            Assert.AreEqual(expected.Total, actual.Total);
            Assert.AreEqual(expected.Limit, actual.Limit);
            Assert.AreEqual(expected.Offset, actual.Offset);
            Assert.AreEqual(expected.Href, actual.Href);
            Assert.AreEqual(expected.Previous, actual.Previous);
            Assert.AreEqual(expected.Next, actual.Next);
            CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
        }
    }
}