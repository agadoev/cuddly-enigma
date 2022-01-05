using System;
using System.Data;
using Domain;
using Application.Repositories;

namespace Application.UseCases {

    public class CreateWishCommand : ICommand {

        public bool Success {get; set;}
        public bool Done {get; set;}

        public Guid UserId {get; set;}

        public Guid? CreatedWishId {get; set;}

        public string WishTitle {get; set;}

        public string WishUrl {get; set;}

        public CreateWishCommand() {
            Success = false;
            Done = false;
            UserId = Guid.Empty;
            CreatedWishId = Guid.Empty;
            WishTitle = string.Empty;
            WishUrl = string.Empty;
        }
    }

    public class CreateWishHandler : ICommandHandler<CreateWishCommand> {

        private readonly IUserRepository _repository;

        public CreateWishHandler(
            IUserRepository repository
        ) {
            _repository = repository;
        }        

        public CreateWishCommand Execute(CreateWishCommand command) {

            if (string.IsNullOrEmpty(command.WishTitle))
                throw new ArgumentNullException("У объекта Wish должно быть заполенно property Title");

            var user = _repository.Get(command.UserId);

            if (user is null)
                throw new RowNotInTableException("Пользователь не найден");

            var wish = new Wish() {
                Title = command.WishTitle,
                Url = command.WishUrl,
                UserId = user.Id
            };

            user.Wishlist.Add(wish);

            command.CreatedWishId = wish.Id;
            command.Success = true;
            command.Done = true;

            return command;
        }
    }
}