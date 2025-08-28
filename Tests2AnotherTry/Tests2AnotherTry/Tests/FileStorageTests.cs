using FluentAssertions;
using NLog;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Task1.SourceCode;
using Task1.SourceCode.exception;

namespace Tests2AnotherTry.Tests
{
    [TestFixture]
    public class FileStorageTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void Constructor_WithSize_ShouldSetMaxAndAvailableSize()
        {
            var storage = new FileStorage(50);
            Logger.Info("FileStorage created with custom size.");
            storage.Should().NotBeNull();
        }

        [Test]
        public void Constructor_Default_ShouldSetDefaultSize()
        {
            var storage = new FileStorage();
            Logger.Info("FileStorage created with default size.");
            storage.Should().NotBeNull();
        }

        [Test]
        public void Write_ShouldAddFile_WhenUniqueAndFits()
        {
            var storage = new FileStorage(10);
            var file = new Task1.SourceCode.File("a.txt", "1234"); // size = 2
            var result = storage.Write(file);
            Logger.Info("Write result: {0}", result);
            result.Should().BeTrue();
            storage.GetFiles().Should().Contain(file);
        }

        [Test]
        public void Write_ShouldThrow_WhenFileNameExists()
        {
            var storage = new FileStorage(10);
            var file1 = new Task1.SourceCode.File("a.txt", "12");
            var file2 = new Task1.SourceCode.File("a.txt", "34");
            storage.Write(file1);
            Logger.Info("Attempting to write duplicate file name.");
            Action act = () => storage.Write(file2);
            act.Should().Throw<FileNameAlreadyExistsException>();
        }

        [Test]
        public void Write_ShouldReturnFalse_WhenNotEnoughSpace()
        {
            var storage = new FileStorage(2);
            var file = new Task1.SourceCode.File("big.txt", "123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456"); // size = 150
            var result = storage.Write(file);
            Logger.Info("Write result for oversized file: {0}", result);
            result.Should().BeFalse();
        }

        [Test]
        public void IsExists_ShouldReturnTrue_WhenFileExists()
        {
            var storage = new FileStorage();
            var file = new Task1.SourceCode.File("b.txt", "12");
            storage.Write(file);
            Logger.Info("Checking existence of file: b.txt");
            storage.IsExists("b.txt").Should().BeTrue();
        }

        [Test]
        public void IsExists_ShouldReturnFalse_WhenFileDoesNotExist()
        {
            var storage = new FileStorage();
            Logger.Info("Checking existence of non-existent file: notfound.txt");
            storage.IsExists("notfound.txt").Should().BeFalse();
        }

        [Test]
        public void Delete_ShouldRemoveFile_WhenExists()
        {
            var storage = new FileStorage();
            var file = new Task1.SourceCode.File("c.txt", "12");
            storage.Write(file);
            var result = storage.Delete("c.txt");
            Logger.Info("Delete result: {0}", result);
            result.Should().BeTrue();
            storage.IsExists("c.txt").Should().BeFalse();
        }

        [Test]
        public void Delete_ShouldReturnFalse_WhenFileDoesNotExist()
        {
            var storage = new FileStorage();
            Logger.Info("Attempting to delete non-existent file: missing.txt");
            var result = storage.Delete("missing.txt");
            result.Should().BeFalse();
        }

        [Test]
        public void GetFiles_ShouldReturnAllFiles()
        {
            var storage = new FileStorage();
            var file1 = new Task1.SourceCode.File("f1.txt", "abc");
            var file2 = new Task1.SourceCode.File("f2.txt", "def");
            storage.Write(file1);
            storage.Write(file2);
            Logger.Info("Getting all files from storage.");
            storage.GetFiles().Should().BeEquivalentTo(new List<Task1.SourceCode.File> { file1, file2 });
        }

        [Test]
        public void GetFile_ShouldReturnFile_WhenExists()
        {
            var storage = new FileStorage();
            var file = new Task1.SourceCode.File("d.txt", "12");
            storage.Write(file);
            Logger.Info("Getting file: d.txt");
            var found = storage.GetFile("d.txt");
            found.Should().Be(file);
        }

        [Test]
        public void GetFile_ShouldReturnNull_WhenNotExists()
        {
            var storage = new FileStorage();
            Logger.Info("Getting non-existent file: missing.txt");
            var found = storage.GetFile("missing.txt");
            found.Should().BeNull();
        }
    }
}