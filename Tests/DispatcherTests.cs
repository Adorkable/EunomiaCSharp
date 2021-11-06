using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace Eunomia.Tests
{
    [TestClass()]
    public class DispatcherTests
    {
        [TestMethod()]
        public void LogUnhandledExceptionsTest()
        {
            var dispatcher = new Dispatcher();

            var count = 0;
            var expectedCount = new Random().Next(1000);
            var expectedExceptionCount = 0;
            for (var queuedCount = 0; queuedCount < expectedCount; queuedCount++)
            {
                dispatcher.Invoke(() =>
                {
                    count += 1;
                    if (count % 2 == 0)
                    {
                        expectedExceptionCount += 1;
                        throw new AssertFailedException();
                    }
                });
            }

            var loggedCount = 0;
            dispatcher.logUnhandledExceptions = (exception) => { loggedCount += 1; };

            dispatcher.InvokePending();

            Assert.AreEqual(count, expectedCount);
            Assert.AreEqual(expectedExceptionCount, loggedCount);
        }

        [TestMethod()]
        public void CatchUnhandledExceptionsTest()
        {
            var dispatcher = new Dispatcher();

            var count = 0;
            var expectedCount = new Random().Next(1000);
            for (var queuedCount = 0; queuedCount < expectedCount; queuedCount++)
            {
                dispatcher.Invoke(() =>
                {
                    count += 1;
                    if (count % 2 == 0)
                    {
                        throw new AssertFailedException();
                    }
                });
            }

            dispatcher.InvokePending();

            Assert.AreEqual(count, expectedCount);
        }

        [TestMethod()]
        public void InvokeTest()
        {
            var dispatcher = new Dispatcher();

            var count = 0;
            var expectedCount = new Random().Next(1000);
            for (var queuedCount = 0; queuedCount < expectedCount; queuedCount++)
            {
                dispatcher.Invoke(() => { count += 1; });
            }

            dispatcher.InvokePending();

            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod()]
        public void InvokePendingTest()
        {
            var dispatcher = new Dispatcher();

            var count = 0;
            var expectedCount = new Random().Next(1000);
            var expectedOrder = new List<int>(expectedCount);
            for (var queuedCount = 0; queuedCount < expectedCount; queuedCount++)
            {
                dispatcher.Invoke(() =>
                {
                    count += 1;
                    expectedOrder.Add(count);
                });
            }

            dispatcher.InvokePending();

            Assert.AreEqual(expectedCount, count);
            for (var orderIndex = 0; orderIndex < expectedOrder.Count - 1; orderIndex++)
            {
                Assert.AreEqual(expectedOrder[orderIndex], expectedOrder[orderIndex + 1] - 1);
            }

            dispatcher.InvokePending();

            Assert.AreEqual(expectedCount, count);
        }
    }
}