using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace SendGrid.SmtpApi.HeaderTests
{
    [TestFixture]
    public class TestRepositoryFiles
    {
        [Test]
        public void TestLicenseEndYear()
        {
            string[] pathsArray = new string[] { "..", "..", "..", "..", "LICENSE.md" };
            string licensePath = Path.Combine(pathsArray);

            string line = File.ReadLines(licensePath).Skip(2).Take(1).First();

            Assert.AreEqual(DateTime.Now.Year.ToString(), line.Substring(14, 4));
        }
    }
}
