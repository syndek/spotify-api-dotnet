using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel.Tests
{
    [TestClass]
    public class ImmutableValueArrayTests
    {
        [TestClass]
        [TestCategory("StructuralEquality")]
        public class StructuralEqualityTests
        {
            [TestMethod]
            public void StructuralEqualityElements_ShouldBeEqual()
            {
                // These 2 arrays *should not* be equal because they *do not* use structural equality.
                var array1 = new[] { "A", "B", "C" };
                var array2 = new[] { "A", "B", "C" };

                Assert.AreNotSame(array1, array2);
                Assert.AreNotEqual(array1, array2);

                // These 2 arrays *should* be equal because they *do* use structural equality.
                var valueArray1 = new ImmutableValueArray<string>(array1);
                var valueArray2 = new ImmutableValueArray<string>(array2);

                Assert.AreNotSame(valueArray1, valueArray2);
                Assert.AreEqual(valueArray1, valueArray2);
            }

            [TestMethod]
            public void ReferenceEqualityElements_ShouldNotBeEqual()
            {
                // These 2 arrays *should not* be equal because they *do not* use structural equality.
                var array1 = new object[] { new(), new(), new() };
                var array2 = new object[] { new(), new(), new() };

                Assert.AreNotSame(array1, array2);
                Assert.AreNotEqual(array1, array2);

                // These 2 arrays also *should not* be equal.
                // Even though they use structural equality, the elements inside *do not*.
                var valueArray1 = new ImmutableValueArray<object>(array1);
                var valueArray2 = new ImmutableValueArray<object>(array2);

                Assert.AreNotSame(valueArray1, valueArray2);
                Assert.AreNotEqual(valueArray1, valueArray2);
            }


            [TestMethod]
            [TestCategory("ObjectModelTypeContext")]
            public void ObjectModelTypeContext_StructuralEqualityValues_ShouldBeEqual()
            {
                // These 2 records *should* be equal as the values inside use structural equality.
                var object1 = new ObjectModelTypeWithList<int>(new(1), new(2), new(3));
                var object2 = new ObjectModelTypeWithList<int>(new(1), new(2), new(3));

                Assert.AreNotSame(object1, object2);
                Assert.AreEqual(object1.Objects, object2.Objects);
                Assert.AreEqual(object1, object2);
            }

            [TestMethod]
            [TestCategory("ObjectModelTypeContext")]
            public void ObjectModelTypeContext_ReferenceEqualityValues_ShouldNotBeEqual()
            {
                // These 2 records *should not* be equal as the values inside use reference equality.
                var object1 = new ObjectModelTypeWithList<object>(new(new()), new(new()), new(new()));
                var object2 = new ObjectModelTypeWithList<object>(new(new()), new(new()), new(new()));

                Assert.AreNotSame(object1, object2);
                Assert.AreNotEqual(object1.Objects, object2.Objects);
                Assert.AreNotEqual(object1, object2);
            }

            private record ObjectModelTypeWithList<TValue>
            {
                public ObjectModelTypeWithList(params ObjectModelTypeWithValue<TValue>[] objects)
                {
                    Objects = new ImmutableValueArray<ObjectModelTypeWithValue<TValue>>(objects);
                }

                public IReadOnlyList<ObjectModelTypeWithValue<TValue>> Objects { get; }
            }

            private record ObjectModelTypeWithValue<TValue>(TValue Value);
        }
    }
}
