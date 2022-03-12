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
        [DataRow("testName", "test-name")]
        [DataRow("test-data", "test-data")]
        [DataRow(" is done", "is-done")]
        [DataRow(" letIt-go ", "let-it-go")]
        [DataRow("-anyBad Name", "any-bad-name")]
        [DataRow("HTTP_METHOD", "http-method")]
        public void TestKebabCase(string source, string result)
        {
            string r = source.ToKebabCase();
            Assert.AreEqual(result, r);
        }

        [TestMethod]
        [DataRow("testName", "testName")]
        [DataRow("test-data", "testData")]
        [DataRow(" is done", "isDone")]
        [DataRow(" letIt-go ", "letItGo")]
        [DataRow("-anyBad Name", "anyBadName")]
        [DataRow("HTTP_METHOD", "httpMethod","httpMETHOD")]
        public void TestCamelCase(string source, string result, string? fresult = null)
        {
            string r = source.ToCamelCase();
            Assert.AreEqual(result, r);
            if (fresult != null)
            {
                string fr = source.ToCamelCase(false);
                Assert.AreEqual(fresult, fr);
            }
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