﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
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

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => seven.Wrap(0.0f));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 5.0f.Wrap(-2.0f));
        }

        [TestMethod()]
        public void ClampTest()
        {
            Assert.AreEqual(5, 10.0f.Clamp(0, 5.0f));
            Assert.AreEqual(4, 4.0f.Clamp(0, 5.0f));
            Assert.AreEqual(0, (-1.0f).Clamp(0, 5.0f));
            Assert.AreEqual(0, 10.0f.Clamp(0, 0));

            Assert.ThrowsException<ArgumentException>(() => 10.0f.Clamp(0, -10));

            // Assert.Fail("Different lower bounds");
        }

        [TestMethod()]
        public void ClampUpperExclusiveTest()
        {
            Assert.AreEqual(4, 10.0f.ClampUpperExclusive(5));
            Assert.AreEqual(4, 4.0f.ClampUpperExclusive(5));
            Assert.AreEqual(0, (-1.0f).ClampUpperExclusive(5));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 10.0f.ClampUpperExclusive(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 10.0f.ClampUpperExclusive(-10));
        }
    }
}