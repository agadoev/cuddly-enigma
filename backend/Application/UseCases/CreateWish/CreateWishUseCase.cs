using Application.UseCases;
using Application.Repositories;

namespace Application.UseCases.CreateWish {

    public class CreateWishUseCase : IUseCase<CreateWishInput, CreateWishOutput> {

        private readonly IUserRepository _repository;

        public CreateWishUseCase(
            IUserRepository repository
        ) {
            _repository = repository;
        }        

        public CreateWishOutput Execute(CreateWishInput input) {
            var output = new CreateWishOutput();


            var user = _repository.Get(input.UserId);

            // кинуть исключение NotFound..

            user.Wishlist.Add(input.Wish);

            output.Success = true;
            return output;
            
        }
    }
}