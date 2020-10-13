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
    }
}