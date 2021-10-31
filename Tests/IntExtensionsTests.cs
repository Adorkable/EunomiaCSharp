using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

            Assert.ThrowsException<ArgumentException>(() => seven.Wrap(0));

            Assert.ThrowsException<ArgumentException>(() => (5).Wrap(-2));
        }

        [TestMethod()]
        public void ClampUpperExclusiveTest()
        {
            Assert.AreEqual(4, 10.ClampUpperExclusive(5));
            Assert.AreEqual(4, 4.ClampUpperExclusive(5));
            Assert.AreEqual(0, (-1).ClampUpperExclusive(5));

            Assert.ThrowsException<OverflowException>(() => 10.ClampUpperExclusive(0));
            Assert.ThrowsException<OverflowException>(() => 10.ClampUpperExclusive(-10));
        }
    }
}