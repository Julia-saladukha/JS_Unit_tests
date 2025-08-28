using Task1.SourceCode;
using Xunit;

namespace Task1.SourceCode.Tests
{
    public class FileTests
    {
        [Fact]
        public void Constructor_SetsFileNameAndContent()
        {
            var file = new File("test.txt", "abcdef");
            Assert.Equal("test.txt", file.GetFileName());
        }

        [Fact]
        public void GetSize_ReturnsHalfContentLength()
        {
            var file = new File("test.txt", "12345678");
            Assert.Equal(4, file.GetSize());
        }

        [Fact]
        public void GetSize_ReturnsZeroForEmptyContent()
        {
            var file = new File("empty.txt", "");
            Assert.Equal(0, file.GetSize());
        }
    }
}