using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SendGrid.SmtpApi.HeaderTests
{
    [TestFixture]
    public class TestUtils
    {
        [Test]
        public void TestSerialize()
        {
            var testcase = "foo";
            String result = Utils.Serialize(testcase);
            Assert.AreEqual("\"foo\"", result);

            var testcase2 = 1;
            result = Utils.Serialize(testcase2);
            Assert.AreEqual("1", result);
        }

        [Test]
        public void TestSerializeDictionary()
        {
            var test = new Dictionary<string, string>
                           {
                               {"a", "b"},
                               {"c", "d/e"}
                           };
            var result = Utils.SerializeDictionary(test);
            var expected = "{\"a\":\"b\",\"c\":\"d/e\"}";
            Assert.AreEqual(expected, result);
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void TestSerializeNullThrowsException()
        {
            string test = null;
            Utils.Serialize(test);
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void TestSerializeNullKeyThrowsException()
        {
            var test = new Dictionary<string, string> { { null, "test" } };
            Utils.Serialize(test);
        }

        [Test]
        public void TestEncodeNonAsciiCharacters()
        {
            var test = "私はラーメンが大好き";
            var result = Utils.EncodeNonAsciiCharacters(test);
            var expected = "\\u79c1\\u306f\\u30e9\\u30fc\\u30e1\\u30f3\\u304c\\u5927\\u597d\\u304d";

            Assert.AreEqual(expected, result);
        }
    }
}