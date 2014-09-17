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
			const string testcase = "foo";
			var result = Utils.Serialize(testcase);
			Assert.AreEqual("\"foo\"", result);

			const int testcase2 = 1;
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
			const string expected = "{\"a\":\"b\",\"c\":\"d\\/e\"}";
			Assert.AreEqual(expected, result);
		}
	}
}