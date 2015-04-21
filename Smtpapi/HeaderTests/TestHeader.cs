using System.Collections.Generic;
using NUnit.Framework;

namespace SendGrid.SmtpApi.HeaderTests
{
    [TestFixture]
    public class TestHeader
    {
        [Test]
        public void TestAddFilterSetting()
        {
            var test = new Header();
            test.AddFilterSetting("foo", new List<string> {"a", "b"}, "bar");
            string result = test.JsonString();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"a\" : {\"b\" : \"bar\"}}}}}", result);
        }

        [Test]
        public void TestAddSection()
        {
            var test = new Header();
            test.AddSection("foo", "bar");
            string result = test.JsonString();
            Assert.AreEqual("{\"section\" : {\"foo\" : \"bar\"}}", result);
        }

        [Test]
        public void TestAddSubstitution()
        {
            var test = new Header();
            test.AddSubstitution("foo", new List<string> {"bar", "raz"});
            string result = test.JsonString();
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"bar\", \"raz\"]}}", result);
        }

        [Test]
        public void TestAddUniqueArgs()
        {
            var test = new Header();
            test.AddUniqueArgs(new Dictionary<string, string> {{"foo", "bar"}});
            string result = test.JsonString();
            Assert.AreEqual("{\"unique_args\" : {\"foo\" : \"bar\"}}", result);
        }

        [Test]
        public void TestDisable()
        {
            var test = new Header();
            test.DisableFilter("foo");
            string result = test.JsonString();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"enable\" : \"0\"}}}}", result);
        }

        [Test]
        public void TestEnable()
        {
            var test = new Header();
            test.EnableFilter("foo");
            string result = test.JsonString();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"enable\" : \"1\"}}}}", result);
        }

        [Test]
        public void TestJsonString()
        {
            var test = new Header();
            string result = test.JsonString();
            Assert.AreEqual("", result);

            test = new Header();
            test.AddSubstitution("foo", new List<string> {"a", "b"});
            result = test.JsonString();
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"a\", \"b\"]}}", result);
        }

        [Test]
        public void TestSetCategories()
        {
            var test = new Header();
            test.SetCategories(new List<string> {"dogs", "animals", "pets", "mammals"});
            string result = test.JsonString();
            Assert.AreEqual("{\"category\" : [\"dogs\", \"animals\", \"pets\", \"mammals\"]}", result);
        }

        [Test]
        public void TestSetCategory()
        {
            var test = new Header();
            test.SetCategory("foo");
            string result = test.JsonString();
            Assert.AreEqual("{\"category\" : \"foo\"}", result);
        }

        [Test]
        public void TestSetTo()
        {
            var test = new Header();
            test.SetTo(new List<string> {"joe@example.com", "jane@example.com"});
            string result = test.JsonString();
            Assert.AreEqual("{\"to\" : [\"joe@example.com\", \"jane@example.com\"]}", result);
        }

        [Test]
        public void TestSetASMGroupID()
        {
            var test = new Header();
            test.SetASMGroupID(123);
            string result = test.JsonString();
            Assert.AreEqual("{\"asm_group_id\" : 123}", result);
        }
    }
}