using System;
using System.Collections.Generic;
using Task1.SourceCode;
using Task1.SourceCode.exception;
using NUnit.Framework;

namespace Task1.SourceCode.Tests
{
    [TestFixture]
    public class FileStorageTests
    {
        [Test]
        public void Write_AddsFile_WhenUniqueAndFits()
        {
            var storage = new FileStorage(10);
            var file = new File("a.txt", "1234"); // size = 2
            var result = storage.Write(file);
            Assert.IsTrue(result);
            Assert.Contains(file, storage.GetFiles());
        }

        [Test]
        public void Write_Throws_WhenFileNameExists()
        {
            var storage = new FileStorage(10);
            var file1 = new File("a.txt", "12");
            var file2 = new File("a.txt", "34");
            storage.Write(file1);
            Assert.Throws<FileNameAlreadyExistsException>(() => storage.Write(file2));
        }

        [Test]
        public void Write_ReturnsFalse_WhenNotEnoughSpace()
        {
            var storage = new FileStorage(2);
            var file = new File("big.txt", "123456"); // size = 3
            var result = storage.Write(file);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsExists_ReturnsTrue_WhenFileExists()
        {
            var storage = new FileStorage();
            var file = new File("b.txt", "12");
            storage.Write(file);
            Assert.IsTrue(storage.IsExists("b.txt"));
        }

        [Test]
        public void IsExists_ReturnsFalse_WhenFileDoesNotExist()
        {
            var storage = new FileStorage();
            Assert.IsFalse(storage.IsExists("notfound.txt"));
        }

        [Test]
        public void Delete_RemovesFile()
        {
            var storage = new FileStorage();
            var file = new File("c.txt", "12");
            storage.Write(file);
            var result = storage.Delete("c.txt");
            Assert.IsTrue(result);
            Assert.IsFalse(storage.IsExists("c.txt"));
        }

        [Test]
        public void GetFile_ReturnsFile_WhenExists()
        {
            var storage = new FileStorage();
            var file = new File("d.txt", "12");
            storage.Write(file);
            var found = storage.GetFile("d.txt");
            Assert.AreEqual(file, found);
        }

        [Test]
        public void GetFile_ReturnsNull_WhenNotExists()
        {
            var storage = new FileStorage();
            var found = storage.GetFile("missing.txt");
            Assert.IsNull(found);
        }
    }
}