using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel.Tests
{
    [TestClass]
    public class ImmutableValueDictionaryTests : Object
    {
        [TestMethod]
        public void StructuralEquality_StructuralEqualityElements()
        {
            // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
            var dictionary1 = new Dictionary<String, Int32> { { "A", 1 }, { "B", 2 }, { "C", 3 } };
            var dictionary2 = new Dictionary<String, Int32> { { "A", 1 }, { "B", 2 }, { "C", 3 } };

            Assert.AreNotSame(dictionary1, dictionary2);
            Assert.AreNotEqual(dictionary1, dictionary2);

            // These 2 dictionaries *should* be equal because they *do* use structural equality.
            var valueDictionary1 = new ImmutableValueDictionary<String, Int32>(dictionary1);
            var valueDictionary2 = new ImmutableValueDictionary<String, Int32>(dictionary2);

            Assert.AreNotSame(valueDictionary1, valueDictionary2);
            Assert.AreEqual(valueDictionary1, valueDictionary2);
        }

        [TestMethod]
        public void StructuralEquality_ReferenceEqualityElements()
        {
            // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
            var dictionary1 = new Dictionary<Object, Object> { { new(), new() }, { new(), new() }, { new(), new() } };
            var dictionary2 = new Dictionary<Object, Object> { { new(), new() }, { new(), new() }, { new(), new() } };

            Assert.AreNotSame(dictionary1, dictionary2);
            Assert.AreNotEqual(dictionary1, dictionary2);

            // These 2 dictionaries also *should not* be equal.
            // Even though they use structural equality, the elements inside *do not*.
            var valueDictionary1 = new ImmutableValueDictionary<Object, Object>(dictionary1);
            var valueDictionary2 = new ImmutableValueDictionary<Object, Object>(dictionary2);

            Assert.AreNotSame(valueDictionary1, valueDictionary2);
            Assert.AreNotEqual(valueDictionary1, valueDictionary2);
        }

        [TestMethod]
        public void StructuralEquality_ReferenceEqualityKeys_StructuralEqualityValues()
        {
            // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
            var dictionary1 = new Dictionary<Object, Int32> { { new(), 1 }, { new(), 2 }, { new(), 3 } };
            var dictionary2 = new Dictionary<Object, Int32> { { new(), 1 }, { new(), 2 }, { new(), 3 } };

            Assert.AreNotSame(dictionary1, dictionary2);
            Assert.AreNotEqual(dictionary1, dictionary2);

            // These 2 dictionaries also *should not* be equal.
            // Even though they use structural equality, the keys inside *do not*.
            var valueArray1 = new ImmutableValueDictionary<Object, Int32>(dictionary1);
            var valueArray2 = new ImmutableValueDictionary<Object, Int32>(dictionary2);

            Assert.AreNotSame(valueArray1, valueArray2);
            Assert.AreNotEqual(valueArray1, valueArray2);
        }

        [TestMethod]
        public void StructuralEquality_StructuralEqualityKeys_ReferenceEqualityValues()
        {
            // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
            var dictionary1 = new Dictionary<String, Object> { { "A", new() }, { "B", new() }, { "C", new() } };
            var dictionary2 = new Dictionary<String, Object> { { "A", new() }, { "B", new() }, { "C", new() } };

            Assert.AreNotSame(dictionary1, dictionary2);
            Assert.AreNotEqual(dictionary1, dictionary2);

            // These 2 dictionaries also *should not* be equal.
            // Even though they use structural equality, the values inside *do not*.
            var valueArray1 = new ImmutableValueDictionary<String, Object>(dictionary1);
            var valueArray2 = new ImmutableValueDictionary<String, Object>(dictionary2);

            Assert.AreNotSame(valueArray1, valueArray2);
            Assert.AreNotEqual(valueArray1, valueArray2);
        }

        [TestMethod]
        public void StructuralEquality_ObjectModelTypeContext_StructuralEqualityElements()
        {
            // These 2 records *should* be equal as the values inside use structural equality.
            var object1 = new ObjectModelTypeWithDictionary<String, Int32>(new Dictionary<String, ObjectModelTypeWithValue<Int32>>()
            {
                { "A", new(1) },
                { "B", new(2) },
                { "C", new(3) }
            });
            var object2 = new ObjectModelTypeWithDictionary<String, Int32>(new Dictionary<String, ObjectModelTypeWithValue<Int32>>()
            {
                { "A", new(1) },
                { "B", new(2) },
                { "C", new(3) }
            });

            Assert.AreNotSame(object1, object2);
            Assert.AreEqual(object1.Objects, object2.Objects);
            Assert.AreEqual(object1, object2);
        }

        [TestMethod]
        public void StructuralEquality_ObjectModelTypeContext_ReferenceEqualityElements()
        {
            // These 2 records *should not* be equal as the values inside use reference equality.
            var object1 = new ObjectModelTypeWithDictionary<Object, Object>(new Dictionary<Object, ObjectModelTypeWithValue<Object>>()
            {
                { new(), new(new()) },
                { new(), new(new()) },
                { new(), new(new()) }
            });
            var object2 = new ObjectModelTypeWithDictionary<Object, Object>(new Dictionary<Object, ObjectModelTypeWithValue<Object>>()
            {
                { new(), new(new()) },
                { new(), new(new()) },
                { new(), new(new()) }
            });

            Assert.AreNotSame(object1, object2);
            Assert.AreNotEqual(object1.Objects, object2.Objects);
            Assert.AreNotEqual(object1, object2);
        }

        [TestMethod]
        public void StructuralEquality_ObjectModelTypeContext_ReferenceEqualityKeys_StructuralEqualityValues()
        {
            // These 2 records *should not* be equal.
            // Even though the values within ObjectModelTypeWithValue use structural equality, the keys do not.
            var object1 = new ObjectModelTypeWithDictionary<Object, Int32>(new Dictionary<Object, ObjectModelTypeWithValue<Int32>>()
            {
                { new(), new(1) },
                { new(), new(2) },
                { new(), new(3) }
            });
            var object2 = new ObjectModelTypeWithDictionary<Object, Int32>(new Dictionary<Object, ObjectModelTypeWithValue<Int32>>()
            {
                { new(), new(1) },
                { new(), new(2) },
                { new(), new(3) }
            });

            Assert.AreNotSame(object1, object2);
            Assert.AreNotEqual(object1.Objects, object2.Objects);
            Assert.AreNotEqual(object1, object2);
        }

        [TestMethod]
        public void StructuralEquality_ObjectModelTypeContext_StructuralEqualityKeys_ReferenceEqualityValues()
        {
            // These 2 records *should not* be equal.
            // Even though the keys use structural equality, the values within ObjectModelTypeWithValue do not.
            var object1 = new ObjectModelTypeWithDictionary<String, Object>(new Dictionary<String, ObjectModelTypeWithValue<Object>>()
            {
                { "A", new(new()) },
                { "B", new(new()) },
                { "C", new(new()) }
            });
            var object2 = new ObjectModelTypeWithDictionary<String, Object>(new Dictionary<String, ObjectModelTypeWithValue<Object>>()
            {
                { "A", new(new()) },
                { "B", new(new()) },
                { "C", new(new()) }
            });

            Assert.AreNotSame(object1, object2);
            Assert.AreNotEqual(object1.Objects, object2.Objects);
            Assert.AreNotEqual(object1, object2);
        }

        private record ObjectModelTypeWithDictionary<TKey, TValue>
        {
            public ObjectModelTypeWithDictionary(IReadOnlyDictionary<TKey, ObjectModelTypeWithValue<TValue>> objects)
            {
                this.Objects = new ImmutableValueDictionary<TKey, ObjectModelTypeWithValue<TValue>>(objects);
            }

            public IReadOnlyDictionary<TKey, ObjectModelTypeWithValue<TValue>> Objects { get; }
        }

        private record ObjectModelTypeWithValue<TValue>(TValue Value);
    }
}