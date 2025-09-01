using Task1.SourceCode;
using NUnit.Framework;

namespace Task1.SourceCode.Tests
{
    [TestFixture]
    public class FileTests
    {
        [Test]
        public void Constructor_SetsFileNameAndContent()
        {
            var file = new File("test.txt", "abcdef");
            Assert.AreEqual("test.txt", file.GetFileName());
        }

        [Test]
        public void GetSize_ReturnsHalfContentLength()
        {
            var file = new File("test.txt", "12345678");
            Assert.AreEqual(4, file.GetSize());
        }

        [Test]
        public void GetSize_ReturnsZeroForEmptyContent()
        {
            var file = new File("empty.txt", "");
            Assert.AreEqual(0, file.GetSize());
        }
    }
}