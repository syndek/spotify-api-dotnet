using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel.Tests
{
    [TestClass]
    public class ImmutableValueArrayTests : Object
    {
        [TestMethod]
        public void StructuralEquality_StructuralEqualityElements()
        {
            // These 2 arrays *should not* be equal because they *do not* use structural equality.
            var array1 = new[] { "A", "B", "C" };
            var array2 = new[] { "A", "B", "C" };

            Assert.AreNotSame(array1, array2);
            Assert.AreNotEqual(array1, array2);

            // These 2 arrays *should* be equal because they *do* use structural equality.
            var valueArray1 = new ImmutableValueArray<String>(array1);
            var valueArray2 = new ImmutableValueArray<String>(array2);

            Assert.AreNotSame(valueArray1, valueArray2);
            Assert.AreEqual(valueArray1, valueArray2);
        }

        [TestMethod]
        public void StructuralEquality_ReferenceEqualityElements()
        {
            // These 2 arrays *should not* be equal because they *do not* use structural equality.
            var array1 = new Object[] { new(), new(), new() };
            var array2 = new Object[] { new(), new(), new() };

            Assert.AreNotSame(array1, array2);
            Assert.AreNotEqual(array1, array2);

            // These 2 arrays also *should not* be equal.
            // Even though they use structural equality, the elements inside *do not*.
            var valueArray1 = new ImmutableValueArray<Object>(array1);
            var valueArray2 = new ImmutableValueArray<Object>(array2);

            Assert.AreNotSame(valueArray1, valueArray2);
            Assert.AreNotEqual(valueArray1, valueArray2);
        }
    }
}