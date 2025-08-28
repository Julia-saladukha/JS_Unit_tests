using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using NLog;
using NUnit.Framework;
using System;
using Task1.SourceCode;

namespace Tests2AnotherTry.Tests
{
    [TestFixture]
    public class FileTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void Constructor_ShouldSetFileNameAndContent()
        {
            var file = new Task1.SourceCode.File("test.txt", "abc");
            Logger.Info("Created file with name: {0}", file.GetFileName());
            file.GetFileName().Should().Be("test.txt");
        }

        [Test]
        public void GetSize_ShouldReturnHalfContentLength()
        {
            var file = new Task1.SourceCode.File("test.txt", "123456");
            Logger.Info("File size: {0}", file.GetSize());
            file.GetSize().Should().Be(3);
        }

        [Test]
        public void GetSize_ShouldReturnZero_WhenContentIsEmpty()
        {
            var file = new Task1.SourceCode.File("empty.txt", "");
            Logger.Info("File size for empty content: {0}", file.GetSize());
            file.GetSize().Should().Be(0);
        }

        [Test]
        public void GetFileName_ShouldReturnFileName()
        {
            var file = new Task1.SourceCode.File("myfile.doc", "content");
            Logger.Info("File name: {0}", file.GetFileName());
            file.GetFileName().Should().Be("myfile.doc");
        }

        [Test]
        public void GetSize_ShouldTruncateDecimal_WhenContentLengthIsOdd()
        {
            // Длина = 5, половина = 2.5, но после приведения к int → 2
            var file = new Task1.SourceCode.File("odd.txt", "12345");
            file.GetSize().Should().Be(2);
        }

        [Test]
        public void Constructor_ShouldSetExtensionCorrectly()
        { 
            // На случай если будут разные форматы
            var file = new Task1.SourceCode.File("document.pdf", "hello world");
            file.GetExtension().Should().Be("pdf");
        }
    }
}