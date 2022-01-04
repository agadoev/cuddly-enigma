using System;
using System.Data;
using Application.UseCases;
using Domain;
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

            if (string.IsNullOrEmpty(input.WishTitle))
                throw new ArgumentNullException("У объекта Wish должно быть заполенно property Title");

            var user = _repository.Get(input.UserId);

            if (user is null)
                throw new RowNotInTableException("Пользователь не найден");

            var wish = new Wish() {
                Title = input.WishTitle,
                Url = input.WishUrl
            };

            user.Wishlist.Add(wish);

            var output = new CreateWishOutput();
            output.Success = true;
            return output;
        }
    }
}