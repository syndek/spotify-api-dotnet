using System;
using System.Collections.Generic;

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


        [TestMethod]
        public void StructuralEquality_ObjectModelTypeContext_StructuralEqualityValues()
        {
            // These 2 records *should* be equal as the values inside use structural equality.
            var object1 = new ObjectModelTypeWithList<Int32>(new(1), new(2), new(3));
            var object2 = new ObjectModelTypeWithList<Int32>(new(1), new(2), new(3));

            Assert.AreNotSame(object1, object2);
            Assert.AreEqual(object1.Objects, object2.Objects);
            Assert.AreEqual(object1, object2);
        }

        [TestMethod]
        public void StructuralEquality_ObjectModelTypeContext_ReferenceEqualityValues()
        {
            // These 2 records *should not* be equal as the values inside use reference equality.
            var object1 = new ObjectModelTypeWithList<Object>(new(new()), new(new()), new(new()));
            var object2 = new ObjectModelTypeWithList<Object>(new(new()), new(new()), new(new()));

            Assert.AreNotSame(object1, object2);
            Assert.AreNotEqual(object1.Objects, object2.Objects);
            Assert.AreNotEqual(object1, object2);
        }

        private record ObjectModelTypeWithList<TValue>
        {
            public ObjectModelTypeWithList(params ObjectModelTypeWithValue<TValue>[] objects)
            {
                this.Objects = new ImmutableValueArray<ObjectModelTypeWithValue<TValue>>(objects);
            }

            public IReadOnlyList<ObjectModelTypeWithValue<TValue>> Objects { get; }
        }

        private record ObjectModelTypeWithValue<TValue>(TValue Value);
    }
}