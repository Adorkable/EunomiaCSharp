using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eunomia.Tests
{
    [TestClass()]
    public class FloatExtensionsTests
    {
        [TestMethod()]
        public void WrapTest()
        {
            float seven = 7;

            Assert.AreEqual(1.0f, seven.Wrap(3.0f));

            Assert.AreEqual(0.0f, seven.Wrap(7.0f));
            Assert.AreEqual(7.0f, seven.Wrap(10.0f));

            Assert.AreEqual(2.0f, (-3.0f).Wrap(5.0f));

            Assert.ThrowsException<ArgumentException>(() => seven.Wrap(0.0f));

            Assert.ThrowsException<ArgumentException>(() => (5.0f).Wrap(-2.0f));
        }

        [TestMethod()]
        public void ClampTest()
        {
            Assert.AreEqual(4, 10.0f.Clamp(0, 5.0f));
            Assert.AreEqual(4, 4.0f.Clamp(0, 5.0f));
            Assert.AreEqual(0, (-1.0f).Clamp(0, 5.0f));

            Assert.ThrowsException<OverflowException>(() => 10.0f.Clamp(0, 0));
            Assert.ThrowsException<OverflowException>(() => 10.0f.Clamp(0, -10));

            // Assert.Fail("Different lower bounds");
        }

        // [TestMethod()]
        // public void MapTest()
        // {
        //     Assert.Fail();
        // }
    }
}