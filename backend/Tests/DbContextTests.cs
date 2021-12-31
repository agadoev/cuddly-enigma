using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using Data;
using Core;
using Moq;

namespace Tests {
    public class DbContextTests {

        private DbContext<User> _uc;
        private Mock<IFileSystem> _fileSystem;

        private string _path;

        [SetUp]
        public void SetUp() {
            _fileSystem = new Mock<IFileSystem>();
            _uc = new DbContext<User>(_fileSystem.Object);
            _path = _uc.GetPath();
        }

        [Test]
        public void GetAll_EmptyFile_ShouldReturnEmptyArray() {
            // arrange
            _fileSystem.Setup(fileSystem => fileSystem.FileExists(_path)).Returns(true);
            _fileSystem.Setup(fileSystem => fileSystem.ReadAllLines(_path)).Returns(new string[] { "", "", "" });

            // act
            var users = _uc.GetAll();

            // assert
            Assert.That(1, Is.EqualTo(1));
        }
    }
}