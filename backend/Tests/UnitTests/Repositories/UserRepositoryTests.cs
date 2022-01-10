using Domain;
using Moq;
using NUnit.Framework;
using Infrastructure.InMemoryDataAcces;
using Infrastructure.InMemoryDataAcces.Repositories;
using System.Collections.Generic;
using Application.Repositories;

namespace Tests.UnitTests.Repositories {
    public class UserRepositoryTest {

        private Mock<InMemoryDbContext> _contextMock;

        private IUserRepository _repository;


        [SetUp]
        public void SetUp() {
            _contextMock = new Mock<InMemoryDbContext>();
            _repository = new UserRepository(_contextMock.Object);
        }


        [Test]
        public void AddUser_ShouldCallAddMethodOfDbContext() {

            _contextMock.Setup(ctx => ctx.Users).Returns(new List<User>());
            var user = new User() {
                Name = "Alex"
            };

            _repository.Add(user);

            _contextMock.Verify((ctx) => ctx.Users, Times.Once);
        }
    }
}