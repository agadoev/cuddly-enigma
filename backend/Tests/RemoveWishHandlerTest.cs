using NUnit.Framework;
using Moq;

namespace Tests {
    public class RemoveWishHandlerTest {


        [Test]
        public void UserIdDoesNotMatch_ShouldThrowException() {

        }

       [Test]
        public void CommandCorrect_ShouldCallGetMethodOfUserRepository() {

        }

        // [Test]
        // public void CommandCorrect_ShouldCallGetMethodOfWishesRepository() {

        // }

        [Test]
        public void CommandCorrect_ShouldCallRemoveMethodOfWishesRepository() {

        }

        [Test]
        public void WishNotFound_ShouldThowObjectNotFoundException() {

        }
    }
}