using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eunomia.Tests
{
    [TestClass()]
    public class IntExtensionsTests
    {
        [TestMethod()]
        public void WrapTest()
        {
            int seven = 7;

            Assert.AreEqual(1, seven.Wrap(3));

            Assert.AreEqual(0, seven.Wrap(7));
            Assert.AreEqual(7, seven.Wrap(10));

            Assert.AreEqual(2, (-3).Wrap(5));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => seven.Wrap(0));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => (5).Wrap(-2));
        }

        [TestMethod()]
        public void ClampTest()
        {
            Assert.AreEqual(5, 10.Clamp(0, 5));
            Assert.AreEqual(4, 4.Clamp(0, 5));
            Assert.AreEqual(0, (-1).Clamp(0, 5));
            Assert.AreEqual(0, 10.Clamp(0, 0));

            Assert.ThrowsException<ArgumentException>(() => 10.Clamp(0, -10));

            // Assert.Fail("Different lower bounds");
        }

        [TestMethod()]
        public void ClampUpperExclusiveTest()
        {
            Assert.AreEqual(4, 10.ClampUpperExclusive(5));
            Assert.AreEqual(4, 4.ClampUpperExclusive(5));
            Assert.AreEqual(0, (-1).ClampUpperExclusive(5));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 10.ClampUpperExclusive(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 10.ClampUpperExclusive(-10));
        }
    }
}