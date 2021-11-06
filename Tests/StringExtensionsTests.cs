using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace Eunomia.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void RemoveXmlTagsTest()
        {
            const string inside = "lalalal";
            var testWith = $"<hi I am a tag>{inside}</closeTaG>";
            Assert.AreEqual(inside, testWith.RemoveXmlTags());

            Assert.AreEqual(inside, inside.RemoveXmlTags());

            string testNull = null;
            Assert.AreEqual(null, StringExtensions.RemoveXmlTags(testNull));

            var spaces = "    ";
            Assert.AreEqual(spaces, spaces.RemoveXmlTags());
        }
    }
}