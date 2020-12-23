using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Collections;
using System.Collections.Generic;

namespace Spotify.ObjectModel.Tests
{
    [TestClass]
    public class ImmutableValueDictionaryTests
    {
        [TestClass]
        [TestCategory("StructuralEquality")]
        public class StructuralEqualityTests
        {
            [TestMethod]
            public void StructuralEqualityElements_ShouldBeEqual()
            {
                // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
                var dictionary1 = new Dictionary<string, int>
                {
                    { "A", 1 },
                    { "B", 2 },
                    { "C", 3 }
                };
                var dictionary2 = new Dictionary<string, int>
                {
                    { "A", 1 },
                    { "B", 2 },
                    { "C", 3 }
                };

                Assert.AreNotSame(dictionary1, dictionary2);
                Assert.AreNotEqual(dictionary1, dictionary2);

                // These 2 dictionaries *should* be equal because they *do* use structural equality.
                var valueDictionary1 = new ImmutableValueDictionary<string, int>(dictionary1);
                var valueDictionary2 = new ImmutableValueDictionary<string, int>(dictionary2);

                Assert.AreNotSame(valueDictionary1, valueDictionary2);
                Assert.AreEqual(valueDictionary1, valueDictionary2);
            }

            [TestMethod]
            public void ReferenceEqualityElements_ShouldNotBeEqual()
            {
                // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
                var dictionary1 = new Dictionary<object, object>
                {
                    { new(), new() },
                    { new(), new() },
                    { new(), new() }
                };
                var dictionary2 = new Dictionary<object, object>
                {
                    { new(), new() },
                    { new(), new() },
                    { new(), new() }
                };

                Assert.AreNotSame(dictionary1, dictionary2);
                Assert.AreNotEqual(dictionary1, dictionary2);

                // These 2 dictionaries also *should not* be equal.
                // Even though they use structural equality, the elements inside *do not*.
                var valueDictionary1 = new ImmutableValueDictionary<object, object>(dictionary1);
                var valueDictionary2 = new ImmutableValueDictionary<object, object>(dictionary2);

                Assert.AreNotSame(valueDictionary1, valueDictionary2);
                Assert.AreNotEqual(valueDictionary1, valueDictionary2);
            }

            [TestMethod]
            public void ReferenceEqualityKeys_StructuralEqualityValues_ShouldNotBeEqual()
            {
                // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
                var dictionary1 = new Dictionary<object, int>
                {
                    { new(), 1 },
                    { new(), 2 },
                    { new(), 3 }
                };
                var dictionary2 = new Dictionary<object, int>
                {
                    { new(), 1 },
                    { new(), 2 },
                    { new(), 3 }
                };

                Assert.AreNotSame(dictionary1, dictionary2);
                Assert.AreNotEqual(dictionary1, dictionary2);

                // These 2 dictionaries also *should not* be equal.
                // Even though they use structural equality, the keys inside *do not*.
                var valueArray1 = new ImmutableValueDictionary<object, int>(dictionary1);
                var valueArray2 = new ImmutableValueDictionary<object, int>(dictionary2);

                Assert.AreNotSame(valueArray1, valueArray2);
                Assert.AreNotEqual(valueArray1, valueArray2);
            }

            [TestMethod]
            public void StructuralEqualityKeys_ReferenceEqualityValues_ShouldNotBeEqual()
            {
                // These 2 dictionaries *should not* be equal because they *do not* use structural equality.
                var dictionary1 = new Dictionary<string, object>
                {
                    { "A", new() },
                    { "B", new() },
                    { "C", new() }
                };
                var dictionary2 = new Dictionary<string, object>
                {
                    { "A", new() },
                    { "B", new() },
                    { "C", new() }
                };

                Assert.AreNotSame(dictionary1, dictionary2);
                Assert.AreNotEqual(dictionary1, dictionary2);

                // These 2 dictionaries also *should not* be equal.
                // Even though they use structural equality, the values inside *do not*.
                var valueArray1 = new ImmutableValueDictionary<string, object>(dictionary1);
                var valueArray2 = new ImmutableValueDictionary<string, object>(dictionary2);

                Assert.AreNotSame(valueArray1, valueArray2);
                Assert.AreNotEqual(valueArray1, valueArray2);
            }

            [TestMethod]
            [TestCategory("ObjectModelTypeContext")]
            public void ObjectModelTypeContext_StructuralEqualityElements_ShouldBeEqual()
            {
                // These 2 records *should* be equal as the values inside use structural equality.
                var object1 = new ObjectModelTypeWithDictionary<string, int>(new Dictionary<string, ObjectModelTypeWithValue<int>>()
                {
                    { "A", new(1) },
                    { "B", new(2) },
                    { "C", new(3) }
                });
                var object2 = new ObjectModelTypeWithDictionary<string, int>(new Dictionary<string, ObjectModelTypeWithValue<int>>()
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
            [TestCategory("ObjectModelTypeContext")]
            public void ObjectModelTypeContext_ReferenceEqualityElements_ShouldNotBeEqual()
            {
                // These 2 records *should not* be equal as the values inside use reference equality.
                var object1 = new ObjectModelTypeWithDictionary<object, object>(new Dictionary<object, ObjectModelTypeWithValue<object>>()
                {
                    { new(), new(new()) },
                    { new(), new(new()) },
                    { new(), new(new()) }
                });
                var object2 = new ObjectModelTypeWithDictionary<object, object>(new Dictionary<object, ObjectModelTypeWithValue<object>>()
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
            [TestCategory("ObjectModelTypeContext")]
            public void ObjectModelTypeContext_ReferenceEqualityKeys_StructuralEqualityValues_ShouldNotBeEqual()
            {
                // These 2 records *should not* be equal.
                // Even though the values within ObjectModelTypeWithValue use structural equality, the keys do not.
                var object1 = new ObjectModelTypeWithDictionary<object, int>(new Dictionary<object, ObjectModelTypeWithValue<int>>()
                {
                    { new(), new(1) },
                    { new(), new(2) },
                    { new(), new(3) }
                });
                var object2 = new ObjectModelTypeWithDictionary<object, int>(new Dictionary<object, ObjectModelTypeWithValue<int>>()
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
            [TestCategory("ObjectModelTypeContext")]
            public void ObjectModelTypeContext_StructuralEqualityKeys_ReferenceEqualityValues_ShouldNotBeEqual()
            {
                // These 2 records *should not* be equal.
                // Even though the keys use structural equality, the values within ObjectModelTypeWithValue do not.
                var object1 = new ObjectModelTypeWithDictionary<string, object>(new Dictionary<string, ObjectModelTypeWithValue<object>>()
                {
                    { "A", new(new()) },
                    { "B", new(new()) },
                    { "C", new(new()) }
                });
                var object2 = new ObjectModelTypeWithDictionary<string, object>(new Dictionary<string, ObjectModelTypeWithValue<object>>()
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
                    Objects = new ImmutableValueDictionary<TKey, ObjectModelTypeWithValue<TValue>>(objects);
                }

                public IReadOnlyDictionary<TKey, ObjectModelTypeWithValue<TValue>> Objects { get; }
            }

            private record ObjectModelTypeWithValue<TValue>(TValue Value);
        }
    }
}
