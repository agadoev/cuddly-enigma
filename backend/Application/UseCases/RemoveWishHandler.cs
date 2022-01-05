using System;
using System.Data;
using Application.Repositories;

namespace Application.UseCases {

    public class RemoveWishCommand : ICommand {
        public bool Success {get; set;}

        public bool Done {get; set;}

        public Guid WishId {get; set;}

        public Guid UserId {get; set;}

        public RemoveWishCommand() {
            Success = false;
            Done = false;
            WishId = Guid.Empty;
            UserId = Guid.Empty;
        }
    }

    public class RemoveWishHandler : ICommandHandler<RemoveWishCommand>{

        private readonly IUserRepository _userRepository;
        private readonly IWishesRepository _wishesRepository;

        public RemoveWishHandler(IUserRepository userRepository, IWishesRepository wishesRepository) {
            _userRepository = userRepository;
            _wishesRepository = wishesRepository;
        }

        public RemoveWishCommand Execute(RemoveWishCommand command) {

            var wish = _wishesRepository.Get(command.WishId);

            if (wish is null)
                throw new RowNotInTableException();

            if (wish.UserId != command.UserId)
                throw new UnauthorizedAccessException();

            _wishesRepository.Remove(command.WishId);

            command.Success = true;
            command.Done = true;

            return command;
        }
    }
}