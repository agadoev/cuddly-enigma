using Domain;
using System;
using NUnit.Framework;
using Moq;
using Application.UseCases;
using Application.UseCases.CreateWish;
using Application.Repositories;

namespace Tests {
    public class CreateWishUseCaseTest {

        private IUseCase<CreateWishInput, CreateWishOutput> _createWishUseCase;

        private Mock<IUserRepository> _userRepository;

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void Test1() {


            // создать пользователя
            var user = new User();
            var userId = user.Id;

            // создать репозиторий
            var repositoryMock = new Mock<IUserRepository>();

            // сказать, что репозиторий будет содержать одного пользователя с пустым вишлистом
            repositoryMock
                .Setup(r => r.Get(userId))
                .Returns(() => user);
            
            // создать Input 
            var input = new CreateWishInput() {
                UserId = userId,
                Wish = new Wish {
                    Title = "Подарочек"
                }
            };

            // создать сам usecase
            var useCase = new CreateWishUseCase(repositoryMock.Object);

            // добавить виш через useCase 
            var output = useCase.Execute(input);

            // проверить, что output.Success == true
            Assert.That(output.Success, Is.True);

            // проверить, что репозиторий возращзает пользователя с непустым списком
            var actualUser = repositoryMock.Object.Get(userId);
            Assert.That(actualUser.Wishlist, Is.Not.Empty);
        }
    }
}