using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eunomia;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eunomia.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void RemoveXMLTagsTest()
        {
            var inside = "lalalal";
            var testWith = $"<hi I am a tag>{inside}</closeTaG>";
            Assert.AreEqual(inside, testWith.RemoveXMLTags());

            Assert.AreEqual(inside, inside.RemoveXMLTags());

            string testNull = null;
            Assert.AreEqual(null, StringExtensions.RemoveXMLTags(testNull));

            var spaces = "    ";
            Assert.AreEqual(spaces, spaces.RemoveXMLTags());
        }
    }
}