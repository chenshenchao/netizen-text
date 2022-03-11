using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Netizen.Text.Test
{
    [TestClass]
    public class NamingStyleTest
    {
        [TestMethod]
        [DataRow("testName", "test", "Name" )]
        [DataRow("HTTP_METHOD", "HTTP", "METHOD")]
        public void TestToWords(string source, params string[] result)
        {
            string[] r = source.ToWords();
            Assert.IsTrue(result.SequenceEqual(r));
        }

        [TestMethod]
        [DataRow("testName", "test_name")]
        [DataRow("test-data", "test_data")]
        [DataRow(" is done", "is_done")]
        [DataRow(" letIt-go ", "let_it_go")]
        [DataRow("-anyBad Name", "any_bad_name")]
        [DataRow("HTTP_METHOD", "http_method")]
        public void TestToSnakeCase(string source, string result)
        {
            string r = source.ToSnakeCase();
            Assert.AreEqual(result, r);
        }

        [TestMethod]
        [DataRow("testName", "TestName")]
        [DataRow("test-data", "TestData")]
        [DataRow(" is done", "IsDone")]
        [DataRow(" letIt-go ", "LetItGo")]
        [DataRow("-anyBad Name", "AnyBadName")]
        [DataRow("HTTP_METHOD", "HttpMethod", "HTTPMETHOD")]
        public void TestToPascalCase(string source, string result, string? fresult=null)
        {
            string r = source.ToPascalCase();
            Assert.AreEqual(result, r);
            if (fresult != null)
            {
                string fr = source.ToPascalCase(false);
                Assert.AreEqual(fresult, fr);
            }
        }
    }
}