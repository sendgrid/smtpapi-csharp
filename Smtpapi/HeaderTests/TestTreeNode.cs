using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SendGrid.SmtpApi.HeaderTests
{
    [TestFixture]
    public class TestTreeNode
    {
        [Test]
        public void TestAddArray()
        {
            var test = new HeaderSettingsNode();
            test.AddArray(new List<string> {"foo", "bar"}, new[] {"raz", "blam"});
            IEnumerable<object> result = test.GetArray("foo", "bar");
            IList<object> enumerable = result as IList<object> ?? result.ToList();
                // Fix for possible multiple enumerations
            Assert.AreEqual(enumerable.ToList()[0], "raz");
            Assert.AreEqual(enumerable.ToList()[1], "blam");
        }

        [Test]
        public void TestAddSetting()
        {
            var test = new HeaderSettingsNode();
            test.AddSetting(new List<string>(), "foo");
            Assert.AreEqual("foo", test.GetLeaf(), "Get the leaf of the first node");

            test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo"}, "bar");
            Assert.AreEqual("bar", test.GetSetting(new List<string> {"foo"}),
                "Get the item in the first branch 'foo', make sure its set to 'bar'");

            test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo"}, "bar");
            Assert.AreEqual("bar", test.GetSetting("foo"),
                "tests the convienence get setting function that omits the lists stuff...");

            test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo", "bar", "raz"}, "foobar");
            Assert.AreEqual("foobar", test.GetSetting("foo", "bar", "raz"),
                "tests a tree that is multiple branches deep");

            test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo", "bar", "raz"}, "foobar");
            test.AddSetting(new List<string> {"barfoo", "barbar", "barraz"}, "barfoobar");
            Assert.AreEqual("foobar", test.GetSetting("foo", "bar", "raz"), "tests a tree that has multiple branches");
            Assert.AreEqual("barfoobar", test.GetSetting("barfoo", "barbar", "barraz"), "tests the other branch");

            test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo"}, "bar");
            try
            {
                test.AddSetting(new List<string> {"foo", "raz"}, "blam");
                Assert.Fail("exception not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Attempt to overwrite setting", ex.Message);
            }
        }

        [Test]
        public void TestIsEmpty()
        {
            var test = new HeaderSettingsNode();
            Assert.IsTrue(test.IsEmpty());

            test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo"}, "bar");
            Assert.IsFalse(test.IsEmpty());

            test = new HeaderSettingsNode();
            test.AddArray(new List<string> {"raz"}, new List<string> {"blam"});
            Assert.IsFalse(test.IsEmpty());
        }

        [Test]
        public void TestToJson()
        {
            var test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo", "bar", "raz"}, "foobar");

            string result = test.ToJson();
            Assert.AreEqual("{\"foo\" : {\"bar\" : {\"raz\" : \"foobar\"}}}", result);

            test = new HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo", "bar", "raz"}, "foobar");
            test.AddSetting(new List<string> {"barfoo", "barbar", "barraz"}, "barfoobar");

            result = test.ToJson();
            Assert.AreEqual(
                "{\"foo\" : {\"bar\" : {\"raz\" : \"foobar\"}},\"barfoo\" : {\"barbar\" : {\"barraz\" : \"barfoobar\"}}}",
                result);

            test = new HeaderSettingsNode();
            test.AddArray(new List<string> {"foo"}, new List<string> {"bar", "raz"});
            result = test.ToJson();
            Assert.AreEqual("{\"foo\" : [\"bar\", \"raz\"]}", result);
        }
    }
}